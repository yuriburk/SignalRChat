using MediatR;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.API.Controllers;
using SignalRChat.API.Hubs;
using SignalRChat.Applications.Features.Annotations.Handlers;
using SignalRChat.Applications.Features.AnnotationsSolicitations.Handlers;
using System.Threading.Tasks;

namespace SignalRChat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnotationsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public AnnotationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return await HandleResult(() => _mediator.Send(new AnnontationsCollection.Query()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AnnotationsCreate.Command command)
        {
            return await HandleResult(() => _mediator.Send(command));
        }
    }
}
