using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.API.Controllers;
using SignalRChat.Applications.Features.Messages.Handlers;
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
        public async Task<IActionResult> GetAllAsync([FromQuery] int? limit)
        {
            return await HandleResult(() => _mediator.Send(new MessagesCollection.Query(limit)));
        }
    }
}
