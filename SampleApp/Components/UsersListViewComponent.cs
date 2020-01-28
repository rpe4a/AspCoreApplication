using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Components
{
    public class UsersList : ViewComponent
    {
        private readonly List<string> users;

        public UsersList()
        {
            users = new List<string>
            {
                "Tom", "Tim", "Bob", "Sam"
            };
        }

        public IViewComponentResult Invoke()
        {
            int number = users.Count;
            // если передан параметр number
            if (Request.Query.ContainsKey("number"))
            {
                Int32.TryParse(Request.Query["number"].ToString(), out number);
            }

            ViewBag.Users = users.Take(number);
            ViewData["Header"] = $"Количество пользователей: {number}.";
            return View();

        }
    }
}