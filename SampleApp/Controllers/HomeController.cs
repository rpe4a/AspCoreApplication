﻿using System.Linq;
using EntitiesLib;
using EntitiesLib.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Models;

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
            var principal = HttpContext.User;

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

        public IActionResult GetUserAgent([FromHeader(Name = "User-Agent")] string userAgent)
        {
            return Content(userAgent);
        }

        public ActionResult GetMessage()
        {
            return PartialView("_GetMessage");
        }

        [Authorize(Policy = "OnlyForMicrosoft")]
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