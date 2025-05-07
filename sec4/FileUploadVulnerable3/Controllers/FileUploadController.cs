using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace FileUploadVulnerable.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");

            if (file.ContentType != "image/jpeg")
                return Content("Only JPEG files are allowed");

            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, Path.GetFileName(file.FileName));
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return Content($"[Header Check] File uploaded to {filePath}");
        }
    }
}
