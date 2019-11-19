using Microsoft.AspNetCore.SignalR;
using SignalRChat.Domain.Features.Annotations;
using System;
using System.Threading.Tasks;

namespace SignalRChat.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Annotation annotation)
        {
            await Clients.All.SendAsync("sendMessage", annotation);
        }

        public override Task OnConnectedAsync()
        {
            SendMessage(new Annotation() { Name = Context.User.Identity.Name, Text = "se conectou ao chat." }).Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            SendMessage(new Annotation() { Name = Context.User.Identity.Name, Text = "se desconectou do chat." }).Wait();
            return base.OnConnectedAsync();
        }
    }
}
