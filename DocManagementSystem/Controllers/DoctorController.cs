using System.Diagnostics;
using System.Numerics;
using DocManagementSystem.Common.Data;
using DocManagementSystem.Common.Models.Api.Request;
using DocManagementSystem.Common.Models.Api.Response;
using DocManagementSystem.Core.Repositories;
using DocManagementSystem.Core.Repositories.Interfaces;
using DoctorManagementSystem.Common.Entities.Models;
using DoctorManagementSystem.Common.Entities.Models.Entities;
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
        var patients = _apiRepository.GetAllEntityData<PatientVM>();
        var patientData = patients.Result.Results as List<PatientVM>;
        var allEntityData = await _apiRepository.GetAllEntityData<DoctorVM>();
        if (allEntityData == null)
        {
            return NotFound(ApiResponse.NotFound("No doctors found"));
        }
        return View("doctorManagement", allEntityData);
    }

    [HttpPost]
    [Route("/AddDoctor")]
    public async Task<IActionResult> AddDoctor([FromBody] AddDoctorRequest request)
    {
        if (request == null) { 
            NotFound(ApiResponse.NotFound("request is not found"));
        }
      
        
        var doctorEntity = new DoctorVM
        {
            FullName = request.FullName,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            Specialty = request.Specialty,
            Department = request.Department, 
        };

        bool response = await _apiRepository.AddEntityData(doctorEntity);
        if (!response)
        {
            return BadRequest(ApiResponse.BadRequest("Failed to add doctor"));
        }
        return Ok(ApiResponse.Okay("Doctor Added successfully"));
    }















    public IActionResult Empty()
    {
        return View("empty");
    }

}
