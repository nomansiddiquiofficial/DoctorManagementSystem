using System.Diagnostics;
using DoctorManagementSystem.Common.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocManagementSystem.Controllers;

public class DoctorController : Controller
{
    private readonly ILogger<DoctorController> _logger;

    public DoctorController(ILogger<DoctorController> logger)
    {
        _logger = logger;
    }
    Doctor doctor = new Doctor();

    [Route("manage-doctors")]
    public IActionResult Index()
    {
        return View("doctorManagement");
    }

    public IActionResult Empty()
    {
        return View("empty");
    }

}
