using System.Collections.Generic;
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
            return View(users);
        }
    }
}