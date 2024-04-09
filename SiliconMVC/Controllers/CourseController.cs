using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
