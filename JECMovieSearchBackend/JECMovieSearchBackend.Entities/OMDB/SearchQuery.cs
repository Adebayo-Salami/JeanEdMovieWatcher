using JECMovieSearchBackend.Entities.Common;

#nullable disable

namespace JECMovieSearchBackend.Entities.OMDB
{
    public class SearchQuery : BaseEntity
    {
        public string SearchKeyword { get; set; }
        public DateTime DateLastSearched { get; set; }
    }
}
