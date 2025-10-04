using JECMovieSearchBackend.Core.ViewModels.MovieRatingVMs;
using JECMovieSearchBackend.Entities.OMDB;

#nullable disable

namespace JECMovieSearchBackend.Core.ViewModels.MovieVMs
{
    public class MovieVM
    {
        public string ImdbID { get; set; }
        public string Poster { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Type { get; set; }
        public string ImdbRating { get; set; }
        public string ImdbVotes { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Awards { get; set; }
        public List<MovieRatingVM> Ratings { get; set; }

        public static implicit operator MovieVM(Movie model)
        {
            return model == null
                ? null
                : new MovieVM
                {
                    ImdbID = model.ImdbID,
                    Poster = model.Poster,
                    Title = model.Title,
                    Year = model.Year,
                    Rated = model.Rated,
                    Released = model.Released,
                    Runtime = model.Runtime,
                    Genre = model.Genre,
                    Type = model.Type,
                    ImdbRating = model.ImdbRating,
                    ImdbVotes = model.ImdbVotes,
                    Country = model.Country,
                    Director = model.Director,
                    Writer = model.Writer,
                    Actors = model.Actors,
                    Awards = model.Awards,
                    Ratings = model.Ratings?.Select(x => (MovieRatingVM)x).ToList() ?? []
                };
        }
    }
}
