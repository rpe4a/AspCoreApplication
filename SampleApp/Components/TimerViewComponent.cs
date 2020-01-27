using System;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Services;

namespace SampleApp.Components
{
    public class Timer : ViewComponent
    {
        private readonly TimeService _service;

        public Timer(TimeService service)
        {
            _service = service;
        }

        public IViewComponentResult Invoke(bool includeSeconds)
        {
            if (includeSeconds)
                return Content($"Текущее время: {_service.GetTime()}");
            else
                return Content($"Текущее время: {DateTime.Now.ToString("hh:mm")}");
        }
    }
}