using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiliconMVC.ViewModels;

namespace SiliconMVC.Controllers;

public class AuthController(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, DataContext dataContext) : Controller
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly DataContext _dataContext = dataContext;

    [HttpGet]
    public IActionResult Signup()
    {
        ViewData["Title"] = "New Account";

        var viewModel = new SignUpViewModel();

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Signup(SignUpViewModel viewModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (!await _dataContext.Users.AnyAsync(x => x.Email == viewModel.Form.Email))
                {
                    var userEntity = new UserEntity
                    {
                        Email = viewModel.Form.Email,
                        UserName = viewModel.Form.Email,
                        FirstName = viewModel.Form.FirstName,
                        LastName = viewModel.Form.LastName,
                        RegistrationDate = DateTime.Now
                    };

                    if ((await _userManager.CreateAsync(userEntity, viewModel.Form.Password)).Succeeded)
                    {
                        if ((await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, false, false)).Succeeded)
                            return RedirectToAction("Index", "Home");
                        else
                            return LocalRedirect("/signin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User account could not be created!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User already exists!");
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect entry!");
            }
        }
        catch(Exception ex) 
        { 
            Console.WriteLine(ex.Message);
            ModelState.AddModelError("","Error!");
        }
        
        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Signin(string returnUrl)
    {
        ViewData["Title"] = "Sign In";

        var viewModel = new SignInViewModel();

        ViewData["ReturnUrl"] = returnUrl ?? "/";
        
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Signin(SignInViewModel viewModel, string returnUrl)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if((await _signInManager.PasswordSignInAsync(viewModel.Form.Email, viewModel.Form.Password, viewModel.Form.Remember, false)).Succeeded)
                    return LocalRedirect(returnUrl);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        ViewData["ReturnUrl"] = returnUrl;
        ModelState.AddModelError("", "Email or password incorrect!");
        return View(viewModel);
    }

    public new async Task<IActionResult> SignOut()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
