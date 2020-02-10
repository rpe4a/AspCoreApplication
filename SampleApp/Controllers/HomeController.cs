using System;
using System.Globalization;
using System.Linq;
using EntitiesLib;
using EntitiesLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SampleApp.Models;

namespace SampleApp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        private readonly IStringLocalizer _stringLocalizer;

        public HomeController(AppDbContext context, IStringLocalizer stringLocalizer)
        {
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var principal = HttpContext.User;

            ViewData["Title"] = _stringLocalizer["Header"];
            ViewData["Message"] = _stringLocalizer["Message"];

            return View(_context.Phones.ToList());
        }

        [HttpGet]
        public string GetCulture()
        {
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
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

        public IActionResult GetUserAgent([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Content(userAgent);
        }

        public ActionResult GetMessage()
        {
            return PartialView("_GetMessage");
        }

        public IActionResult Create()
        {
            var principal = HttpContext.User;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (ModelState.IsValid)
                return Content($"{person.Name} - {person.Email}");
            else
                return View(person);
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckEmail(string email)
        {
            return Json(email != "admin@mail.ru" && email != "aaa@gmail.com");
        }
    }
}