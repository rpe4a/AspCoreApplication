using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SampleApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send", message).ConfigureAwait(false);
        }

        public override Task OnConnectedAsync()
        {
            var client = this.Clients;
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}