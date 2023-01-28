using DataAccess.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace MaximProject.Areas.admin.Controllers
{
    [Area("admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        { 
            return View(_context.services);
        }

    }
}
