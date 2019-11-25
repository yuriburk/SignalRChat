using MediatR;
using Microsoft.AspNetCore.SignalR;
using SignalRChat.Applications.Features.Messages.Handlers;
using System;
using System.Threading.Tasks;

namespace SignalRChat.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task SendMessage(MessagesCreate.Command message)
        {
            await _mediator.Send(message);
            await Clients.All.SendAsync("sendMessage", message);
        }

        public override Task OnConnectedAsync()
        {
            var message = new MessagesCreate.Command()
            {
                Name = Context.GetHttpContext().Request.Query["username"],
                Text = "Se conectou ao chat."
            };
            SendMessage(message).Wait();
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var message = new MessagesCreate.Command()
            {
                Name = Context.GetHttpContext().Request.Query["username"],
                Text = "Se desconectou do chat."
            };
            SendMessage(message).Wait();
            return base.OnDisconnectedAsync(exception);
        }
    }
}
