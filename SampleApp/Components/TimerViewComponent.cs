using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Components
{
    public class Timer : ViewComponent
    {
        public Task<string> InvokeAsync()
        {
            return Task.FromResult($"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}");
        }
    }
}