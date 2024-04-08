using Business.DTOs;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers;

public class AuthController(AuthService authService) : Controller
{
    private readonly AuthService _authService = authService;

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
                UserAuthDTO userAuth = new UserAuthDTO
                {
                    Email = viewModel.Form.Email,
                    Password = viewModel.Form.Password
                };
                if (await _authService.SignIn(userAuth))
                {
                    return RedirectToAction("Details", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password incorrect!");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        ViewData["Title"] = "Incorrect entry";
        return View(viewModel);
    }

    public new async Task<IActionResult> SignOut()
    {
        await HttpContext.SignOutAsync();
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
    public async Task<IActionResult> Signup(SignUpViewModel viewModel)
    {
        if (!ModelState.IsValid)
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
                if (await _authService.CreateUserAsync(newUser))
                {
                    ///CLAIMS!!!
                    return RedirectToAction("SignIn", "Auth");
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            ViewData["Title"] = "Error";
            return View(viewModel);
        }
    }
}
