using DocManagementSystem.Common.Models.Api.Response;
using Microsoft.Extensions.Logging;

namespace DocManagementSystem.Core.Repositories
{
    public class ApiRepository
    {
        private readonly ILogger<ApiRepository> logger;
        public ApiRepository(ILogger<ApiRepository> logger)
        {
            this.logger = logger;
        }

        public SearchListing GetSearchData()
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
    }
}