using System.Linq;
using EntitiesLib;
using EntitiesLib.Models;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Model;

namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_context.Phones.ToList());
        }

        [HttpGet]
        public IActionResult Buy(int? id)
        {
            if (id == null) return RedirectToAction("Index");

            ViewBag.PhoneId = id;

            return View();
        }

        [HttpPost]
        public string Buy(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return "Спасибо, " + order.User + ", за покупку!";
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            string authData = $"Login: {login}   Password: {password}";
            return Content(authData);
        }

        public IActionResult AddUser(User user)
        {
            if (ModelState.IsValid)
            {
                string userInfo = $"Id: {user.Id}  Name: {user.Name}  Age: {user.Age}  HasRight: {user.HasRight}";
                return Content(userInfo);
            }

            return Content($"Количество ошибок: {ModelState.ErrorCount}");
        }

        public IActionResult GetUserAgent([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Content(userAgent);
        }

        public ActionResult GetMessage()
        {
            return PartialView("_GetMessage");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] string name, [FromForm] int age)
        {
            return Content($"{name} - {age}");
        }
    }
}