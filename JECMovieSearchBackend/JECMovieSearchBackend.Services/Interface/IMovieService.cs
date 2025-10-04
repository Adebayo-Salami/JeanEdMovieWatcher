using JECMovieSearchBackend.Core.ViewModels.MovieVMs;
using JECMovieSearchBackend.Core.ViewModels.SearchQueryVMs;
using JECMovieSearchBackend.Services.Model;

namespace JECMovieSearchBackend.Services.Interface
{
    public interface IMovieService
    {
        /// <summary>
        /// This method will be used to retrive the full detail of a movie
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        Task<ResultModel<MovieVM>> GetMovieDetail(string movieId);
        /// <summary>
        /// This method will return search history and will be limited to top 5 recent search
        /// </summary>
        /// <returns></returns>
        Task<ResultModel<IEnumerable<SearchQueryVM>>> GetSearchHistory();
        /// <summary>
        /// This method will be used to search for movies
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ResultModel<IEnumerable<MovieVM>>> Search(MovieSearchVM model);
    }
}
