using DocManagementSystem.Common.Models.Api.Response;
using DoctorManagementSystem.Common.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DocManagementSystem.Core.Repositories.Interfaces
{
    public interface IApiRepository
    {
        //Task<SearchListing> GetSearchData();

        Task<SearchListing<T>> GetAllEntityData<T>() where T : class;
        Task<bool> AddEntityData<T>(T entityRequest) where T : class;
        Task<bool> UpdateEntityData<T>(T entityRequest, int id, string entityType) where T : class;

        Task<bool> DeleteEntityData(int id, string entityType);


    }
}