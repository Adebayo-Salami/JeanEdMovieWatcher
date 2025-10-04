using JECMovieSearchBackend.Entities.OMDB;

#nullable disable

namespace JECMovieSearchBackend.Core.ViewModels.SearchQueryVMs
{
    public class SearchQueryVM
    {
        public long Id { get; set; }
        public string SearchKeyword { get; set; }
        public DateTime DateLastSearched { get; set; }

        public static implicit operator SearchQueryVM(SearchQuery model)
        {
            return model == null
                ? null
                : new SearchQueryVM
                {
                    Id = model.Id,
                    SearchKeyword = model.SearchKeyword,
                    DateLastSearched = model.DateLastSearched,
                };
        }
    }
}
