using Azure.Core;
using DocManagementSystem.Common.Data;
using DocManagementSystem.Common.Models.Api.Response;
using DocManagementSystem.Core.Repositories.Interfaces;
using DoctorManagementSystem.Common.Entities.Models;
using DoctorManagementSystem.Common.Entities.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DocManagementSystem.Core.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private readonly ILogger<ApiRepository> logger;
        private readonly ApplicationDbContext _dbContext;
        public ApiRepository(ILogger<ApiRepository> logger, ApplicationDbContext applicationDb)
        {
            this.logger = logger;
            this._dbContext = applicationDb;
        }

        public async Task<SearchListing<T>> GetAllEntityData<T>() where T : class
        {
            try
            {
                var allEntityData = await _dbContext.Set<T>().ToListAsync();
                var RetVal = new SearchListing<T>(allEntityData);

                if (allEntityData.Count == 0)
                    return RetVal;
                return RetVal;
            }
            catch (Exception ex)
            {
                logger.LogError("Error in GetSearchData: " + ex.Message);
                throw new Exception("Error in GetSearchData: " + ex.Message);
            }
        }

        public async Task<bool> AddEntityData<T>(T entity) where T : class
        {
            if (entity == null)
                return false;

            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateEntityData<T>(T entity, int id) where T : class
        {
            try
            {
                var existingEntity = await _dbContext.Set<T>().FindAsync(id);
                if (existingEntity == null)
                {
                    return false;
                }

                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteEntityData<T>(int id) where T : class
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null) return false;

            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}