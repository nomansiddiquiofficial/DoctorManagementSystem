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
        var doctors = _apiRepository.GetAllEntityData<DoctorVM>();
        var doctorsData = doctors.Result.Results as List<DoctorVM>;
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
        bool response = false;
        if (request == null)
        {
            return NotFound(ApiResponse.NotFound("request is not found"));
        }

        var doctors = _apiRepository.GetAllEntityData<DoctorVM>();
        var doctorsData = doctors.Result.Results as List<DoctorVM>;
        var fetchDoctor = doctorsData.FirstOrDefault(d => d.Id == request.DoctorId);
        if (fetchDoctor != null)
        {
            var paitentEntity = new PatientVM
            {
                FullName = request.FullName,
                Age = request.Age,
                Gender = request.Gender,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Doctor = fetchDoctor
            };
            response = await _apiRepository.AddEntityData(paitentEntity);
        }

        if (!response)
        {
            return BadRequest(ApiResponse.BadRequest("Failed to add paitent"));
        }
        return Ok(ApiResponse.Okay("Paitent Added successfully"));
    }















    public IActionResult Empty()
    {
        return View("empty");
    }

}
