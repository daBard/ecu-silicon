using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers
{
    public class UserController(UserService userService) : Controller
    {
        private readonly UserService _userService = userService;

        [HttpGet]
        public IActionResult Signin()
        {
            var viewModel = new SignInViewModel();

            ViewData["Title"] = "Sign In";

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Signin(SignInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserAuthDTO userAuthDTO = new UserAuthDTO
                    {
                        Email = viewModel.Form.Email,
                        Password = viewModel.Form.Password
                    };
                    await _userService.CheckPassword(userAuthDTO);
                }
                catch(Exception ex) { Console.WriteLine(ex.Message); }
                return RedirectToAction("Details", "User");
            }
            ViewData["Title"] = "Incorrect entry";
            return View(viewModel);

            
        }

        [HttpGet]
        public IActionResult Signup()
        {
            var viewModel = new SignUpViewModel();

            ViewData["Title"] = "New Account";

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Signup(SignUpViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                ViewData["Title"] = "Incorrect entry";
                return View(viewModel);
            }
            else
            {
                try
                {
                    UserCreateDTO newUser = new UserCreateDTO 
                    { 
                        FirstName = viewModel.Form.FirstName,
                        LastName = viewModel.Form.LastName,
                        Email = viewModel.Form.Email,
                        Password = viewModel.Form.Password
                    };
                    if (await _userService.CreateUserAsync(newUser))
                    {
                        return RedirectToAction("SignIn", "User");
                    }
                }
                catch(Exception ex) { Console.WriteLine(ex.Message); }
                ViewData["Title"] = "Error";
                return View(viewModel);
            }
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
