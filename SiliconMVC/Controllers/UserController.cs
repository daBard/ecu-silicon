using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Signin()
        {
            ViewData["Title"] = "Login";

            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            var viewModel = new SignUpViewModel();

            ViewData["Title"] = "New Account";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult PostForm(SignUpViewModel model)
        {
            return View();
        }
    }
}
