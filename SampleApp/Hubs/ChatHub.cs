using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SampleApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string message)
        {
            var context = Context;
            var clients = Clients;
            var groups = Groups;
            await Clients.All.SendAsync("Receive", message, Context.ConnectionId);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} вошел в чат");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}