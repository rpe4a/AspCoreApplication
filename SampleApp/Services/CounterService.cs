namespace SampleApp.Services
{
    public class CounterService
    {
        public readonly ICounter _counter;

        public CounterService(ICounter counter)
        {
            _counter = counter;
        }

        public ICounter Counter => _counter;
    }
}