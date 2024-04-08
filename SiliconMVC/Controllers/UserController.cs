using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers
{
    [Authorize]
    public class UserController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        [HttpGet]
        public IActionResult Details()
        {
            var viewModel = new UserDetailsViewModel();

            ViewData["Title"] = "User details";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DetailsSaveBasicInfo()
        {
            var viewModel = new UserDetailsViewModel();

            ViewData["Title"] = "User details";

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DetailsSaveAddress()
        {
            var viewModel = new UserDetailsViewModel();

            ViewData["Title"] = "User details";

            return View(viewModel);
        }
    }
}