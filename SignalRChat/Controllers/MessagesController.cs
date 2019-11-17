using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.API.Controllers;
using SignalRChat.Applications.Features.Messages.Handlers;
using SignalRChat.Applications.Features.MessagesSolicitations.Handlers;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return await HandleResult(() => _mediator.Send(new MessagesCollection.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MessagesCreate.Command command)
        {
            return await HandleResult(() => _mediator.Send(command));
        }
    }
}
