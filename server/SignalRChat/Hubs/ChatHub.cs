using Microsoft.AspNetCore.SignalR;
using SignalRChat.Domain.Features.Messages;
using System;
using System.Threading.Tasks;

namespace SignalRChat.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("sendMessage", message);
        }

        public override Task OnConnectedAsync()
        {
            var message = new Message()
            {
                Name = Context.GetHttpContext().Request.Query["username"],
                Text = "Se conectou ao chat."
            };
            SendMessage(message).Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var message = new Message()
            {
                Name = Context.GetHttpContext().Request.Query["username"],
                Text = "Se desconectou do chat."
            };
            SendMessage(message).Wait();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
