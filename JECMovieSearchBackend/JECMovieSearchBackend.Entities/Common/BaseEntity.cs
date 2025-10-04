using System.ComponentModel.DataAnnotations;

namespace JECMovieSearchBackend.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
