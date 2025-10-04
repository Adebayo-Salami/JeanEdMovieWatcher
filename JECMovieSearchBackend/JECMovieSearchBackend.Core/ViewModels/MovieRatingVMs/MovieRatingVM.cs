using JECMovieSearchBackend.Entities.OMDB;

#nullable disable

namespace JECMovieSearchBackend.Core.ViewModels.MovieRatingVMs
{
    public class MovieRatingVM
    {
        public long Id { get; set; }
        public string Source { get; set; }
        public string Value { get; set; }

        public static implicit operator MovieRatingVM(MovieRating model)
        {
            return model == null
                ? null
                : new MovieRatingVM
                {
                    Id = model.Id,
                    Source = model.Source,
                    Value = model.Value
                };
        }
    }
}
