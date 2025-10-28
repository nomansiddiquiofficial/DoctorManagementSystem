using System.Diagnostics;
using System.Numerics;
using Azure.Core;
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

public class DoctorController : Controller
{
    private readonly ILogger<DoctorController> _logger;
    private IApiRepository _apiRepository;
    private readonly ApplicationDbContext _dbContext;
    public DoctorController(ILogger<DoctorController> logger, IApiRepository apiRepository, ApplicationDbContext applicationDb)
    {
        _logger = logger;
        _apiRepository = apiRepository;
        _dbContext = applicationDb;
    }

    [HttpGet]
    [Route("/GetDoctors")]

    public async Task<IActionResult> GetDoctors()
    {
        try
        {
            var doctors = await _apiRepository.GetEntityData<DoctorVM>(Constants.EntityTypes.DoctorEntityType);

            if (doctors == null)
            {
                return NotFound(ApiResponse.NotFound("No doctors found"));
            }


            var doctorData = new SearchListing<DoctorVM>(
             doctors.Results.Cast<DoctorVM>().ToList());

            return View("doctorManagement", doctors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching doctors");
            return StatusCode(500, ApiResponse.InternalServerError("Failed to get doctors"));
        }
    }

    [HttpPost]
    [Route("/AddDoctor")]
    public async Task<IActionResult> AddDoctor([FromBody] AddDoctorRequest request)
    {
        if (request == null)
        {
            return BadRequest(ApiResponse.BadRequest("Invalid request"));
        }

        try
        {
            bool added = await _apiRepository.AddEntityData(request, Constants.EntityTypes.DoctorEntityType);

            if (!added)
            {
                return BadRequest(ApiResponse.BadRequest("Failed to add doctor"));
            }

            return Ok(ApiResponse.Okay("Doctor added successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding doctor");
            return StatusCode(500, ApiResponse.InternalServerError("Failed to add doctor"));
        }
    }

    [HttpPatch]
    [Route("Doctor/EditDoctor/{id}")]
    public async Task<IActionResult> EditDoctor(int id, [FromBody] EditDoctorRequest request)
    {
        if (request == null)
        {
            return BadRequest(ApiResponse.BadRequest("Invalid request"));
        }

        try
        {
            string entityType = Constants.EntityTypes.DoctorEntityType;
            bool response = await _apiRepository.UpdateEntityData(request, id, entityType);
            if (!response)
            {
                return BadRequest(ApiResponse.BadRequest("Failed to update doctor"));
            }
            return Ok(ApiResponse.Okay("Doctor updated successfully"));
        }
        catch (Exception ex)
        {
            // Optional: log ex
            return StatusCode(500, ApiResponse.InternalServerError("Failed to update doctor"));
        }
    }



    [HttpDelete]
    [Route("Doctor/DeleteDoctor/{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        string entityType = Constants.EntityTypes.DoctorEntityType;
        try
        {
            bool response = await _apiRepository.DeleteEntityData(id, entityType);
            if (!response)
            {
                return BadRequest(ApiResponse.BadRequest("Failed to delete doctor"));
            }
            return Ok(ApiResponse.Okay("Doctor Delete successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.InternalServerError("Failed to delete doctor"));
        }
    }















    public IActionResult Empty()
    {
        return View("empty");
    }

}
