using Azure.Core;
using DocManagementSystem.Common.Data;
using DocManagementSystem.Common.Models.Api.Request;
using DocManagementSystem.Common.Models.Api.Response;
using DocManagementSystem.Core.Repositories.Interfaces;
using DoctorManagementSystem.Common.Entities.Models;
using DoctorManagementSystem.Common.Entities.Models.Entities;
using EAU_be.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata;
using static EAU_be.Common.Helpers.Constants;

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

        public async Task<bool> UpdateEntityData<T>(T editData, int id, string entityType) where T : class
        {
            try
            {
                object existingEntity = null;

                if (entityType == Constants.EntityTypes.DoctorEntityType)
                {
                    existingEntity = await _dbContext.Doctors.FindAsync(id);
                }
                else if (entityType == Constants.EntityTypes.PatientEntityType)
                {
                    existingEntity = await _dbContext.Paitents.FindAsync(id);
                }

                if (existingEntity == null)
                {
                    return false;
                }

                if (entityType == Constants.EntityTypes.DoctorEntityType &&
                    existingEntity is DoctorVM existingDoctor &&
                    editData is EditDoctorRequest newDoctorData)
                {
                    if (!string.IsNullOrEmpty(newDoctorData.FullName))
                        existingDoctor.FullName = newDoctorData.FullName;

                    if (!string.IsNullOrEmpty(newDoctorData.Gender))
                        existingDoctor.Gender = newDoctorData.Gender;

                    if (!string.IsNullOrEmpty(newDoctorData.PhoneNumber))
                        existingDoctor.PhoneNumber = newDoctorData.PhoneNumber;

                    if (!string.IsNullOrEmpty(newDoctorData.Specialty))
                        existingDoctor.Specialty = newDoctorData.Specialty;

                    if (!string.IsNullOrEmpty(newDoctorData.Department))
                        existingDoctor.Department = newDoctorData.Department;

                    if (!string.IsNullOrEmpty(newDoctorData.DocStatus) &&
                        Enum.TryParse(typeof(DoctorVM.Status), newDoctorData.DocStatus, true, out var statusValue))
                    {
                        existingDoctor.DocStatus = (DoctorVM.Status)statusValue;
                    }
                }
                else if (entityType == Constants.EntityTypes.PatientEntityType &&
                         existingEntity is PatientVM existingPatient &&
                         editData is EditPatientRequest newPatientData)
                {
                    if (!string.IsNullOrEmpty(newPatientData.FullName))
                        existingPatient.FullName = newPatientData.FullName;

                    if (newPatientData.Age.HasValue)
                        existingPatient.Age = newPatientData.Age.Value;

                    if (!string.IsNullOrEmpty(newPatientData.Gender))
                        existingPatient.Gender = newPatientData.Gender;

                    if (!string.IsNullOrEmpty(newPatientData.Address))
                        existingPatient.Address = newPatientData.Address;

                    if (!string.IsNullOrEmpty(newPatientData.PhoneNumber))
                        existingPatient.PhoneNumber = newPatientData.PhoneNumber;

                    if (newPatientData.DoctorId.HasValue)
                    {
                        var doctor = await _dbContext.Doctors.FindAsync(newPatientData.DoctorId.Value);
                        if (doctor != null)
                        {
                            existingPatient.DoctorId = newPatientData.DoctorId.Value;
                            existingPatient.Doctor = doctor;
                        }
                    }
                }
                else
                {
                    _dbContext.Entry(existingEntity).CurrentValues.SetValues(editData);
                }

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }


        public async Task<bool> DeleteEntityData(int id, string entityType)
        {
            try
            {
                object existingEntity = null;

                if (entityType == Constants.EntityTypes.DoctorEntityType)
                {
                    existingEntity = await _dbContext.Doctors.FindAsync(id);
                }
                else if (entityType == Constants.EntityTypes.PatientEntityType)
                {
                    existingEntity = await _dbContext.Paitents.FindAsync(id);
                }

                if (existingEntity == null)
                {
                    return false;
                }

                if (entityType == Constants.EntityTypes.DoctorEntityType && existingEntity is DoctorVM existingDoctor)
                {
                    _dbContext.Doctors.Remove(existingDoctor);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                else if (entityType == Constants.EntityTypes.PatientEntityType && existingEntity is PatientVM existingPatient)
                {
                    _dbContext.Paitents.Remove(existingPatient);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
