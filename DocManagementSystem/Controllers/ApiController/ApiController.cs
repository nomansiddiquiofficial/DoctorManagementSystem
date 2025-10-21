using Microsoft.AspNetCore.Mvc;
using DocManagementSystem.Core;
using Microsoft.AspNetCore.Http;
using DocManagementSystem.Common.Data;
using DocManagementSystem.Core.Repositories;
using DocManagementSystem.Common.Models.Api.Response;
using DocManagementSystem.Core.Repositories.Interfaces;

namespace DocManagementSystem
{
    public class ApiController : ControllerBase
    {
        private IApiRepository _repository;
        private readonly ILogger<ApiController> _logger;
        public ApiController(ILogger<ApiController> logger, IApiRepository apiRepository)
        {
            _logger = logger;
            _repository = apiRepository;

        }

    }
}
