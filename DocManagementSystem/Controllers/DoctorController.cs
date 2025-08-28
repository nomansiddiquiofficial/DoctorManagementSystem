using DocManagementSystem.Common.Data;
using Microsoft.AspNetCore.Mvc;

namespace DocManagementSystem.Controllers
{
    public class DoctorController : Controller
    {

        public DoctorController(ApplicationDbContext context)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
