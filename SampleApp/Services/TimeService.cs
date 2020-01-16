using System;

namespace SampleApp.Services
{
    public class TimeService
    {
        public string GetTime()
        {
            return DateTime.UtcNow.ToShortTimeString();
        }
    }
}