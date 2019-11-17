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
            var result = await _mediator.Send(new MessagesCollection.Query());
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] MessagesCreate.Command command)
        {
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
