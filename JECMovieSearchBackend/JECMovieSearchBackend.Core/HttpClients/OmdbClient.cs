using JECMovieSearchBackend.Core.Configuration;
using JECMovieSearchBackend.Core.ViewModels.OmdbClientVMs;
using JECMovieSearchBackend.Entities.OMDB;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

#nullable disable

namespace JECMovieSearchBackend.Core.HttpClients
{
    public class OmdbClient
    {
        private readonly HttpClient _client;

        private readonly string _apiKey;
        private readonly string _baseUrl;

        public OmdbClient(HttpClient client, IOptions<OmdbConfiguration> options)
        {
            _client = client;
            _apiKey = options.Value.ApiKey;
            _baseUrl = options.Value.BaseUrl;
        }

        public async Task<SearchMoviesByTitleResponseVM> SearchMoviesAsync(string title)
        {
            var response = await _client.GetAsync($"{_baseUrl}?apikey={_apiKey}&s={title}");
            response.EnsureSuccessStatusCode();
            var ted = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadFromJsonAsync<SearchMoviesByTitleResponseVM>();
        }

        public async Task<Movie> GetMovieDetailsAsync(string imdbId)
        {
            var response = await _client.GetAsync($"{_baseUrl}?apikey={_apiKey}&i={imdbId}&plot=full");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Movie>();
        }
    }
}
