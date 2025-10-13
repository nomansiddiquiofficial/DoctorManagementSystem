using DocManagementSystem.Common.Models.Api.Response;
using DocManagementSystem.Core.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DocManagementSystem.Core.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private readonly ILogger<ApiRepository> logger;
        public ApiRepository(ILogger<ApiRepository> logger)
        {
            this.logger = logger;
        }
        public async Task<SearchListing> GetSearchData()
        {
            try
            {
                SearchListing searchListing = null;
                return searchListing;
            }
            catch (Exception ex)
            {
                logger.LogError("Error in GetSearchData: " + ex.Message);
                throw new Exception("Error in GetSearchData: " + ex.Message);
            }
        }

        public async Task<List<object>> GetAllEntityData(object EntityType)
        {

            return [];
        }

    }
}