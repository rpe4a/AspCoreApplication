using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SampleApp.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        [Authorize(Roles = "admin")]
        public async Task Send(string message)
        {
            var user = Context.User;
            var context = Context;
            var clients = Clients;
            var groups = Groups;

            await Clients.All.SendAsync("Receive", message, Context.UserIdentifier);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("Notify", $"{Context.UserIdentifier} вошел в чат");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("Notify", $"{Context.UserIdentifier} покинул в чат");
            await base.OnDisconnectedAsync(exception);
        }
    }
}