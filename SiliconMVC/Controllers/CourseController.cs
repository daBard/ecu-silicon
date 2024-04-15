using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SiliconMVC.Models;
using SiliconMVC.ViewModels;


namespace SiliconMVC.Controllers
{
    [Authorize]
    public class CourseController(HttpClient httpClient) : Controller
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IActionResult> Index()
        {
            var viewModel = new CourseViewModel();
            ViewData["Title"] = "Courses";

            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7125/api/course");
                if (response.IsSuccessStatusCode)
                {
                    var courses = JsonConvert.DeserializeObject<IEnumerable<CourseModel>>(await response.Content.ReadAsStringAsync());
                    if (courses != null && courses.Any())
                        viewModel.Courses = courses;
                }
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
            
            return View(viewModel);
        }
    }
}
