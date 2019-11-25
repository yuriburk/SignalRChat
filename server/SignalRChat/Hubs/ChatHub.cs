using MediatR;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Domain.Features.Messages;
using SignalRChat.Infra.NoSQL.Features.Messages;
using System;
using System.Threading.Tasks;

namespace SignalRChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(Message message)
        {
            //_mediator.Send(message);
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
