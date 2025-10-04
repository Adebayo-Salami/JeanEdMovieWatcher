using JECMovieSearchBackend.Core.DataAccess;
using JECMovieSearchBackend.Services.Services;
using NUnit.Framework;

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
            _context = FakeDbContext.GetFakeDbContext();
            _movieService = new MovieService(_context, null, null);
        }

        [TestCase("Trump")]
        [TestCase("Boy")]
        public async Task Search_Movie_Should_Return_MovieList(string keywrod)
        {
            // Arrange

            // Act

            // Assert
        }
    }
}