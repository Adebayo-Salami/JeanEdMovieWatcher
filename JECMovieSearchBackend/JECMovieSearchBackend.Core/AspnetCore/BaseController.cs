using JECMovieSearchBackend.Core.Extensions;
using JECMovieSearchBackend.Core.ViewModels;
using JECMovieSearchBackend.Core.ViewModels.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#nullable disable

namespace JECMovieSearchBackend.Core.AspnetCore
{
    public class BaseController : ControllerBase
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();  // Add Console Logging
            });
            _logger = loggerFactory.CreateLogger<BaseController>();
        }

        public IActionResult ApiResponse<T>(T data = default, string message = "", ApiResponseCodes codes = ApiResponseCodes.OK, int? totalCount = 0, params string[] errors)
        {
            var response = new ApiResponse<T>(data, message, codes, totalCount, errors);
            response.Description = message ?? response.Code.GetDescription();
            return Ok(response);
        }

        protected IActionResult HandleError(Exception ex, string customErrorMessage = null)
        {
            _logger.LogError(ex.StackTrace, ex);

            var rsp = new ApiResponse<string>();
            rsp.Code = ApiResponseCodes.ERROR;
#if DEBUG
            rsp.Description = $"Error: {ex?.InnerException?.Message ?? ex.Message} --> {ex?.StackTrace}";
            return Ok(rsp);
#else
            rsp.Description = customErrorMessage ?? "An error occurred while processing your request!";
            return Ok(rsp);
#endif
        }
    }
}
