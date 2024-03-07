using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Signin()
        {
            var viewModel = new SignInViewModel();

            ViewData["Title"] = "Sign In";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Signin(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Incorrect entry";
                return View(viewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Signup()
        {
            var viewModel = new SignUpViewModel();

            ViewData["Title"] = "New Account";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Signup(SignUpViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewData["Title"] = "Incorrect entry";
                return View(viewModel);
            }

            return RedirectToAction("SignIn", "User");  
        }

        [HttpGet]
        public IActionResult Details()
        {
            var viewModel = new UserDetailsViewModel();

            ViewData["Title"] = "User details";

            return View(viewModel);
        }
    }
}
