using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Courses";
            return View();
        }
    }
}
