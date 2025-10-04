#nullable disable

namespace JECMovieSearchBackend.Core.ViewModels.OmdbClientVMs
{
    public class SearchMoviesByTitleResponseVM
    {
        public List<MovieBasicInfoVM> Search { get; set; }
    }

    public class MovieBasicInfoVM
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
