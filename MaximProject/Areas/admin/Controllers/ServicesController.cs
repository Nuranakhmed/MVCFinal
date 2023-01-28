using Core.Entities;
using DataAccess.Contexts;
using MaximProject.Areas.ViewModels;
using MaximProject.Utilites;
using Microsoft.AspNetCore.Mvc;
using System.Security;

namespace MaximProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ServicesController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.services);
        }
        public IActionResult Detail(int id)
        {
            Services service = _context.services.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            if (service == null)
            {
                return BadRequest();
            }
            return View(service);


        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ServicesVM servicesVM)
        {

            if (servicesVM.Photo == null)
            {
                ModelState.AddModelError("Photo", "yoxdur!");
            }
            if (servicesVM.Photo.CheckFileSize(500))
            {
                ModelState.AddModelError("Photo", "olcunu duz ver!");
            }
            if (servicesVM.Photo.CheckFileType("image"))
            {
                ModelState.AddModelError("Photo", "bu sekil deil salaqmsn cmle?");
            }
            string wwwroot = _environment.WebRootPath;
            var filename =  await servicesVM.Photo.SaveFileAsync(wwwroot, "img", "icons");
            Services db = new Services
            {
                Name = servicesVM.Name,
                Description = servicesVM.Description,
                Photo = filename

            };
            _context.services.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            Services service = _context.services.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            if (service == null)
            {
                return BadRequest();
            }
            return View(service);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Delete(int id, string name=null)
        {
            Services db = _context.services.Find(id);
            if (db == null) return BadRequest();
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "slider", db.Photo);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.services.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update()
        {
            return View();
        }
    }
}

