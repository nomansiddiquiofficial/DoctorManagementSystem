using System.Diagnostics;
using System.Numerics;
using DocManagementSystem.Common.Data;
using DocManagementSystem.Common.Models.Api.Request;
using DocManagementSystem.Common.Models.Api.Response;
using DocManagementSystem.Core.Repositories;
using DocManagementSystem.Core.Repositories.Interfaces;
using DoctorManagementSystem.Common.Entities.Models;
using DoctorManagementSystem.Common.Entities.Models.Entities;
using EAU_be.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocManagementSystem.Controllers;

public class PaitentController : Controller
{
    private readonly ILogger<PaitentController> _logger;
    private IApiRepository _apiRepository;
    private readonly ApplicationDbContext _dbContext;

    public PaitentController(ILogger<PaitentController> logger, IApiRepository apiRepository, ApplicationDbContext applicationDb)
    {
        _logger = logger;
        _apiRepository = apiRepository;
        _dbContext = applicationDb;
    }

    [HttpGet]
    [Route("/GetPaitents")]
    public async Task<IActionResult> GetPaitents()
    {
        var allEntityData = await _apiRepository.GetAllEntityData<PatientVM>();
        var doctors = await _apiRepository.GetAllEntityData<DoctorVM>();
        var doctorsData = doctors.Results as List<DoctorVM>;
        ViewBag.Doctors = doctorsData;

        if (allEntityData == null)
        {
            return NotFound(ApiResponse.NotFound("No Patient found"));
        }
        return View("paitentManagement", allEntityData);
    }

    [HttpPost]
    [Route("/AddPaitent")]
    public async Task<IActionResult> AddPatient([FromBody] AddPaitentRequest request)
    {
        if (request == null)
        {
            return NotFound(ApiResponse.NotFound("request is not found"));
        }

        try
        {
            var doctors = await _apiRepository.GetAllEntityData<DoctorVM>();
            var doctorsData = doctors.Results as List<DoctorVM>;
            var fetchDoctor = doctorsData.FirstOrDefault(d => d.Id == request.DoctorId);

            if (fetchDoctor == null)
            {
                return BadRequest(ApiResponse.BadRequest("Invalid Doctor ID"));
            }

            var paitentEntity = new PatientVM
            {
                FullName = request.FullName,
                Age = request.Age,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                DoctorId = request.DoctorId,
                Doctor = fetchDoctor
            };

            bool response = await _apiRepository.AddEntityData(paitentEntity);

            if (!response)
            {
                return BadRequest(ApiResponse.BadRequest("Failed to add patient"));
            }

            return Ok(ApiResponse.Okay("Patient Added successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.InternalServerError("Failed to add patient"));
        }
    }

    [HttpPatch]
    [Route("Paitent/EditPatient/{id}")]
    public async Task<IActionResult> EditPatient(int id, [FromBody] EditPatientRequest request)
    {
        if (request == null)
        {
            return BadRequest(ApiResponse.BadRequest("Invalid request"));
        }

        try
        {
            string entityType = Constants.EntityTypes.PatientEntityType;
            bool response = await _apiRepository.UpdateEntityData(request, id, entityType);

            if (!response)
            {
                return BadRequest(ApiResponse.BadRequest("Failed to update patient"));
            }

            return Ok(ApiResponse.Okay("Patient updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating patient: {ex.Message}");
            return StatusCode(500, ApiResponse.InternalServerError("Failed to update patient"));
        }
    }

    [HttpDelete]
    [Route("Paitent/DeletePatient/{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        string entityType = Constants.EntityTypes.PatientEntityType;

        try
        {
            bool response = await _apiRepository.DeleteEntityData(id, entityType);

            if (!response)
            {
                return BadRequest(ApiResponse.BadRequest("Failed to delete patient"));
            }

            return Ok(ApiResponse.Okay("Patient deleted successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting patient: {ex.Message}");
            return StatusCode(500, ApiResponse.InternalServerError("Failed to delete patient"));
        }
    }

    public IActionResult Empty()
    {
        return View("empty");
    }
}