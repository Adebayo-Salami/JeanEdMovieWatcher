using JECMovieSearchBackend.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace JECMovieSearchBackend.Entities.OMDB
{
    [Table("MovieRatings", Schema = "OMDB")]
    public class MovieRating : BaseEntity
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}
