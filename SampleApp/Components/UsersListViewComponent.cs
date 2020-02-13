using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Services;

namespace SampleApp.Components
{
    public class UsersList : ViewComponent
    {
        private readonly IUserRepository _repository;

        public UsersList(IUserRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var users = _repository.GetUsers();
            ViewBag.Users = users;
            ViewData["Header"] = $"Количество пользователей: {users.Count}.";
            return View();

        }
    }
}