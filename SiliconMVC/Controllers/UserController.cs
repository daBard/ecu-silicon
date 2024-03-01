using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Signin()
        {
            ViewData["Title"] = "Login";

            return View();
        }

        public IActionResult Signup()
        {
            ViewData["Title"] = "New Account";

            return View();
        }
    }
}
