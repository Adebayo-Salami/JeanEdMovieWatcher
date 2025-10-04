using JECMovieSearchBackend.Core.Caching;
using JECMovieSearchBackend.Core.Configuration;
using JECMovieSearchBackend.Core.DataAccess;
using JECMovieSearchBackend.Core.HttpClients;
using JECMovieSearchBackend.Core.ViewModels.MovieVMs;
using JECMovieSearchBackend.Core.ViewModels.OmdbClientVMs;
using JECMovieSearchBackend.Entities.OMDB;
using JECMovieSearchBackend.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Net;
using System.Net.Http.Json;

namespace JECMovieSearchBackend.Test
{
    [TestFixture]
    internal sealed class MovieServiceTest
    {
        private ApplicationDbContext _context;
        private MovieService _movieService;

        [SetUp]
        public void Setup()
        {
            var config = new OmdbConfiguration
            {
                ApiKey = "2bc8399b",
                BaseUrl = "http://www.omdbapi.com/"
            };
            var fakeJson = "{\"Search\":[{\"Title\":\"That's My Boy\",\"Year\":\"2012\",\"imdbID\":\"tt1232200\",\"Type\":\"movie\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BMTM3NDMyNzgzMV5BMl5BanBnXkFtZTcwMjIyMTA1Nw@@._V1_SX300.jpg\"}],\"totalResults\":\"8\",\"Response\":\"True\"}";
            var fakeResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(fakeJson, System.Text.Encoding.UTF8, "application/json")
            };
            IOptions<OmdbConfiguration> options = Options.Create(config);

            _context = FakeDbContext.GetFakeDbContext();
            _movieService = new MovieService(_context, new MemoryCacheManager(), new OmdbClient(FakeHttpClientFactory.CreateFakeHttpClient(fakeResponseMessage), options));
        }

        [TestCase("That's My Boy")]
        public async Task MovieSearch_Is_GreaterThanZero(string keyword)
        {
            // Arrange

            // Act
            var result = await _movieService.Search(new MovieSearchVM() { Title = keyword });

            // Assert
            Assert.IsFalse(result.HasError);
            Assert.That(result.Data.Count, Is.GreaterThan(0));
        }

        [TestCase]
        public async Task SearchHistory_Returns_FiveMostRecentSearch()
        {
            // Arrange
            List<SearchQuery> searchHistory = [
                new () { SearchKeyword = "Boy", DateLastSearched = DateTime.Now.AddMinutes(-123) },
                new () { SearchKeyword = "Trump", DateLastSearched = DateTime.Now.AddMinutes(-1) },
                new () { SearchKeyword = "Game of Thrones", DateLastSearched = DateTime.Now.AddMinutes(-103) },
                new () { SearchKeyword = "Todller", DateLastSearched = DateTime.Now.AddMinutes(-3) },
                new () { SearchKeyword = "Example", DateLastSearched = DateTime.Now.AddMinutes(-13) },
                new () { SearchKeyword = "JEC", DateLastSearched = DateTime.Now.AddMinutes(-13) },
                new () { SearchKeyword = "Jen", DateLastSearched = DateTime.Now.AddMinutes(-5) },
                new () { SearchKeyword = "Ed", DateLastSearched = DateTime.Now.AddMinutes(-33) },
                new () { SearchKeyword = "Key", DateLastSearched = DateTime.Now.AddMinutes(-2) },
                new () { SearchKeyword = "Hello", DateLastSearched = DateTime.Now.AddMinutes(-4) },
            ];
            await _context.SearchQueries.AddRangeAsync(searchHistory);
            await _context.SaveChangesAsync();
            List<string> expectedResult = ["Trump", "Key", "Todller", "Hello"];
            var all = _context.SearchQueries.ToList();

            // Act
            var result = await _movieService.GetSearchHistory();

            // Assert
            Assert.IsFalse(result.HasError);
            Assert.That(result.Data.Count, Is.EqualTo(5));
            List<string> actualList = result.Data.Select(x => x.SearchKeyword).ToList();
            foreach (var expected in expectedResult)
                Assert.IsTrue(actualList.Contains(expected), $"Expected item '{expected}' was not found in actual list.");
        }
    }
}