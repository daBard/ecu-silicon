using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Welcome";

        return View();
    }

    // !! TESTING

    [HttpPost]
    public IActionResult SubscribeToNewsletter()
    {


        RedirectToAction("Index", "Home");
    }
}
