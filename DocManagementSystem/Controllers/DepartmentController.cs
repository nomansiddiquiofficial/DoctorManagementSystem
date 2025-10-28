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

public class DepartmentController : Controller
{
    private readonly ILogger<DoctorController> _logger;
    private IApiRepository _apiRepository;
    private readonly ApplicationDbContext _dbContext;
    public DepartmentController(ILogger<DoctorController> logger, IApiRepository apiRepository, ApplicationDbContext applicationDb)
    {
        _logger = logger;
        _apiRepository = apiRepository;
        _dbContext = applicationDb;
    }

    [HttpGet]
    [Route("/GetDepartments")]
    public async Task<IActionResult> GetDepartments()
    {
        var allEntityData = await _apiRepository.GetAllEntityData<DepartmentVM>();
        var doctors = await _apiRepository.GetAllEntityData<DoctorVM>();
        var doctorsData = doctors.Results as List<DoctorVM>;
        ViewBag.Doctors = doctorsData;
        if (allEntityData == null)
        {
            return NotFound(ApiResponse.NotFound("No Department found"));
        }
        return View("departmentManagement", allEntityData);
    }
    [HttpPost]
    [Route("/AddDepartment")]
    public async Task<IActionResult> AddDepartment([FromBody] AddDepartmentRequest request)
    {
        if (request == null)
        {
            return NotFound(ApiResponse.NotFound("request is not found"));
        }

        try
        {
            var doctors = await _apiRepository.GetAllEntityData<DoctorVM>();
            var doctorsData = doctors.Results as List<DoctorVM>;
            var fetchDoctor = doctorsData.FirstOrDefault(d => d.Id == request.HeadDoctorId);
            

            if (fetchDoctor == null)
            {
                return BadRequest(ApiResponse.BadRequest("Invalid Doctor ID"));
            }

            //var departmentEntity = new DepartmentVM
            //{
            //    DepartmentName = request.DepartmentName,
            //    Doctors = fetchDoctor,
            //    DoctorId = fetchDoctor.Id,
            //    HeadDoctorName = fetchDoctor.FullName
            //};

            bool response = await _apiRepository.AddEntityData(fetchDoctor);

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
    public IActionResult Empty()
    {
        return View("empty");
    }

}
