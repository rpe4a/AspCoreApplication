using System.Linq;
using EntitiesLib;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Phones.ToList());
        }
    }
}