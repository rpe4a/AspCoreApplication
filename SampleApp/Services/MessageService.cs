namespace SampleApp.Services
{
    public class MessageService
    {
        private readonly IMessageSender _sender;

        public MessageService(IMessageSender sender)
        {
            _sender = sender;
        }

        public string Send()
        {
            return _sender.Send();
        }
    }
}