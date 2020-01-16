using System;

namespace SampleApp.Services
{
    public class RandomCounter : ICounter
    {
        private static readonly Random rnd = new Random();

        public RandomCounter()
        {
            Value = rnd.Next(0, 100000);
        }

        public int Value { get; }
    }
}