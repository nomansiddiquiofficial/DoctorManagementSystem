using Microsoft.AspNetCore.Mvc;
using DocManagementSystem.Core;
using Microsoft.AspNetCore.Http;
using DocManagementSystem.Common.Data;
using DocManagementSystem.Core.Repositories;
using DocManagementSystem.Common.Models.Api.Response;

namespace DocManagementSystem
{
    public class ApiController : ControllerBase
    {
        private ApiRepository _repository;
        private readonly ILogger<ApiController> _logger;
        public ApiController(ILogger<ApiController> logger,ApiRepository apiRepository)
        {
            _logger = logger;
            _repository = apiRepository;

        }

        public async Task<IActionResult> GetSearchData()
        {
            var data = _repository.GetSearchData();
            if (data == null)
            {
                _logger.LogWarning("No data found");
                return NotFound(ApiResponse.NotFound("No data found"));

            }
            _logger.LogInformation("Data fetched successfully");
            return Ok(ApiResponse.Okay(data, "data send successfully"));
        }
    }
}
