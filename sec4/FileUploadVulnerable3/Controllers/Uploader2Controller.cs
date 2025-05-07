using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileUploadVulnerable.Controllers
{
    public class Uploader2Controller : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File not selected");

            var ext = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (ext != ".jpg")
                return Content("Only JPEG files are allowed");

            var fileName = $"{Guid.NewGuid()}{ext}";
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            Directory.CreateDirectory(uploads);
            var filePath = Path.Combine(uploads, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return Content($"[Extension Check GUID] File uploaded to {filePath}");
        }
    }
}
