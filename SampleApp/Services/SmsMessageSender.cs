namespace SampleApp.Services
{
    public class SmsMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Send by SMS!";
        }
    }
}