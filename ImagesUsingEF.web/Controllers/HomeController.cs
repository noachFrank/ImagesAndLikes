using ImagesUsingEF.data;
using ImagesUsingEF.web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImagesUsingEF.web.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _webHostEnvironment;

        private string _connectionString;

        public HomeController(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _webHostEnvironment = webHostEnvironment;
            _connectionString = config.GetConnectionString("ConStr");
        }

        public IActionResult Index()
        {
            var repo = new ImageRepository(_connectionString);

            return View(new AllImagesViewModel
            {
                Images = repo.GetAllImages()
            });
        }

      
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file, string imageName)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);
            using var fs = new FileStream(path, FileMode.CreateNew);
            file.CopyTo(fs);

            var repo = new ImageRepository(_connectionString);
            repo.Upload(fileName, imageName);

            return RedirectToAction("index");
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new ImageRepository(_connectionString);

            var image = repo.GetById(id);

            if (image == null)
            {
                return Redirect("/");
            }

            var ids = HttpContext.Session.Get<List<int>>("ids");

            return View(new ViewImageViewModel
            {
                Image = image,
                Liked = ids != null && ids.Contains(id)
            });
        }

        public IActionResult GetById(int id)
        {
            var repo = new ImageRepository(_connectionString);

            return Json(repo.GetById(id));
        }

        [HttpPost]
        public void Like(int id)
        {
            var repo = new ImageRepository(_connectionString);
            repo.Like(id);

            var ids = HttpContext.Session.Get<List<int>>("ids");
            if (ids == null)
            {
                ids = new List<int>();
            }
            ids.Add(id);
            HttpContext.Session.Set<List<int>>("ids", ids);

        }
    }
}