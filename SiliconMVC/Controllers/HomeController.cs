using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconMVC.Models;
using SiliconMVC.ViewModels;
using System.Text;

namespace SiliconMVC.Controllers;

public class HomeController(HttpClient httpClient) : Controller
{
    private readonly HttpClient _httpClient = httpClient;

    public IActionResult Index()
    {
        ViewData["Title"] = "Welcome";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubscribeToNewsletter(SubscribeViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(model.Subscriber), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7125/api/subscriber", content);
                if (response.IsSuccessStatusCode) 
                    TempData["StatusMessage"] = "You are now subscribed!";
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    TempData["StatusMessage"] = "You are already subscribed!";
                else
                    TempData["StatusMessage"] = "Error - Something went wrong while attempting to subscribe!";
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
                TempData["StatusMessage"] = "Error - Something went wrong while attempting to subscribe!";
            }
        }
        else
            TempData["StatusMessage"] = "Error - Something went wrong while attempting to subscribe!";

        return RedirectToAction("Index", "Home", "newsletter");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveSubscriber(string email = "TEST")
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7125/api/subscriber?email={email}");
            if (response.IsSuccessStatusCode) { }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

        return RedirectToAction("Index", "Home");
    }
}
