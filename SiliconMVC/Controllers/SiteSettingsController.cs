using Microsoft.AspNetCore.Mvc;

namespace SiliconMVC.Controllers
{
    public class SiteSettingsController : Controller
    {
        [HttpPost]
        public IActionResult ConsentCookies()
        {
            var option = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(60),
                HttpOnly = true,
                Secure = true
            };

            Response.Cookies.Append("CookieConsent", "true", option);
            return Ok();
        }
    }
}
