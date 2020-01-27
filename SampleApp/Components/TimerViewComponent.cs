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

        public string Invoke(bool includeSeconds)
        {
            if (includeSeconds)
                return $"Текущее время: {_service.GetTime()}";
            else
                return $"Текущее время: {DateTime.Now.ToString("hh:mm")}";
        }
    }
}