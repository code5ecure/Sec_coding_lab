using Microsoft.AspNetCore.Mvc;

namespace FileUploadVulnerable.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}
