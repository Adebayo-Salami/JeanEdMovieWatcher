using JECMovieSearchBackend.Core.AspnetCore;
using JECMovieSearchBackend.Core.ViewModels;
using JECMovieSearchBackend.Core.ViewModels.Enums;
using JECMovieSearchBackend.Core.ViewModels.MovieVMs;
using JECMovieSearchBackend.Core.ViewModels.SearchQueryVMs;
using JECMovieSearchBackend.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JECMovieSearchBackend.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("movieId")]
        [ProducesResponseType(typeof(ApiResponse<MovieVM>), 200)]
        public async Task<IActionResult> GetSearchHistory(string movieId)
        {
            try
            {
                var result = await _movieService.GetMovieDetail(movieId);
                if (result.HasError)
                    return ApiResponse<MovieVM?>(null, message: result.Message, ApiResponseCodes.FAILED, totalCount: 0, errors: result.GetErrorMessages().ToArray());

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data);
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<SearchQueryVM>>), 200)]
        public async Task<IActionResult> GetSearchHistory()
        {
            try
            {
                var result = await _movieService.GetSearchHistory();
                if (result.HasError)
                    return ApiResponse<IEnumerable<SearchQueryVM>>([], message: result.Message, ApiResponseCodes.FAILED, totalCount: 0, errors: result.GetErrorMessages().ToArray());

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.Count());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MovieVM>>), 200)]
        public async Task<IActionResult> Search([FromQuery] MovieSearchVM model)
        {
            try
            {
                var result = await _movieService.Search(model);
                if (result.HasError)
                    return ApiResponse<IEnumerable<MovieVM>>([], message: result.Message, ApiResponseCodes.FAILED, totalCount: 0, errors: result.GetErrorMessages().ToArray());

                return ApiResponse(message: result.Message, codes: ApiResponseCodes.OK, data: result.Data, totalCount: result.Data.Count());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
