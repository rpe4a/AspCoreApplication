namespace SampleApp.Services
{
    public class EmailMessageSender : IMessageSender
    {
        public string Send()
        {
            return "Send by Email!";
        }
    }
}