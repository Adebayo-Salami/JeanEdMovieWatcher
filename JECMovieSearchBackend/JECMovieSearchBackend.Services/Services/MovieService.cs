using JECMovieSearchBackend.Core.Caching;
using JECMovieSearchBackend.Core.DataAccess;
using JECMovieSearchBackend.Core.HttpClients;
using JECMovieSearchBackend.Core.ViewModels.MovieVMs;
using JECMovieSearchBackend.Core.ViewModels.SearchQueryVMs;
using JECMovieSearchBackend.Services.Interface;
using JECMovieSearchBackend.Services.Model;
using Microsoft.EntityFrameworkCore;
using static JECMovieSearchBackend.Core.Utility.CoreConstants;

namespace JECMovieSearchBackend.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheManager _cacheManager;
        private readonly OmdbClient _omdbClient;

        public MovieService(ApplicationDbContext context, ICacheManager cacheManager, OmdbClient omdbClient)
        {
            _context = context;
            _cacheManager = cacheManager;
            _omdbClient = omdbClient;
        }

        public async Task<ResultModel<MovieVM>> GetMovieDetail(string movieId)
        {
            var resultModel = new ResultModel<MovieVM>();

            try
            {
                var movie = await _context.Movies.AsNoTracking()
                    .Include(x => x.Ratings).AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ImdbID == movieId);

                if(movie == null)
                {
                    var searchResult = await _omdbClient.GetMovieDetailsAsync(movieId);
                    if (searchResult == null)
                        throw new Exception("Error, unable to retrieve movie details");

                    await _context.Movies.AddAsync(searchResult);
                    await _context.SaveChangesAsync();
                    movie = searchResult;
                }
                resultModel.Data = movie;
                resultModel.Message = "Detail Retrived Successfully";
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public async Task<ResultModel<IEnumerable<SearchQueryVM>>> GetSearchHistory()
        {
            var resultModel = new ResultModel<IEnumerable<SearchQueryVM>>();

            try
            {
                var searchHistory = await _cacheManager.AddOrGetAsync(CacheConstants.Keys.MovieSearchHistory,
                    async () =>
                    {
                        return await _context.SearchQueries.AsNoTracking()
                            .OrderByDescending(x => x.DateLastSearched)
                            .Take(5)
                            .ToListAsync();
                    }, CacheConstants.CacheTime.DayInMinutes
                );

                resultModel.Data = [.. searchHistory.Select(x => (SearchQueryVM)x)];
                resultModel.Message = "Success";
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public async Task<ResultModel<IEnumerable<MovieVM>>> Search(MovieSearchVM model)
        {
            var resultModel = new ResultModel<IEnumerable<MovieVM>>();

            try
            {
                if (String.IsNullOrWhiteSpace(model.Title))
                    throw new Exception("No Search Parameter passed.");

                await MaintainSearchHistory(model.Title);
                var searchResult = await _omdbClient.SearchMoviesAsync(model.Title);
                if (searchResult == null)
                    throw new Exception("No result found matching search criteria");

                var movies = searchResult.Search.Select(x => new MovieVM()
                {
                    ImdbID = x.imdbID,
                    Title = x.Title,
                    Year = x.Year,
                    Type = x.Type,
                    Poster = x.Poster,
                }).ToList();
                resultModel.Data = movies;
                resultModel.Message = $"Found {movies.Count} Movie(s) matching search criteria";
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        private async Task MaintainSearchHistory(string keyword)
        {
            _cacheManager.Remove(CacheConstants.Keys.MovieSearchHistory);
            var searchHistory = await _context.SearchQueries.AsNoTracking()
                .FirstOrDefaultAsync(x => x.SearchKeyword == keyword);

            if (searchHistory == null)
                await _context.SearchQueries.AddAsync(new()
                {
                    SearchKeyword = keyword,
                    DateLastSearched = DateTime.Now
                });
            else
            {
                searchHistory.DateLastSearched = DateTime.Now;
                _context.SearchQueries.Update(searchHistory);
            }
            await _context.SaveChangesAsync();
        }
    }
}
