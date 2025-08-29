using DocManagementSystem.Common.Models.Api.Response;
using Microsoft.Extensions.Logging;

namespace DocManagementSystem.Core.Repositories.Interfaces
{
    public interface IApiRepository
    {
        Task<SearchListing> GetSearchData();
       
    }
}