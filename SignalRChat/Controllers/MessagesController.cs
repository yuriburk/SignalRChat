using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.API.Controllers;
using SignalRChat.API.Hubs;
using SignalRChat.Applications.Features.Messages.Handlers;
using SignalRChat.Applications.Features.MessagesSolicitations.Handlers;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ApiControllerBase
    {
        private readonly ChatHub _hub;
        private readonly IMediator _mediator;

        public MessagesController(ChatHub hub, IMediator mediator)
        {
            _hub = hub;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _mediator.Send(new MessagesCollection.Query());
            return HandleResult(result);
        }

        [HttpPost]
        public async Task CreateAsync([FromBody] MessagesCreate.Command command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                await _hub.SendMessage(result.Success);
        }
    }
}
