using Microsoft.AspNetCore.Mvc;
using SignalRChat.Domain.Base.Results;
using SignalRChat.Infra.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.API.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        [HttpGet]
        [Route("api/is-alive")]
        public ActionResult IsAlive()
        {
            return Ok(true);
        }

        protected async Task<IActionResult> HandleResult<TSuccess, TFailure>(Func<Task<Result<TSuccess, TFailure>>> func) where TFailure : Exception
        {
            var result = await func();
            if (result.IsFailure)
                return HandleFailure(result.Failure);

            return Ok(result.Success);
        }

        protected IActionResult HandleFailure(Exception error)
        {
            return StatusCode(ErrorCodes.Unhandled.GetHashCode(), error.Message);
        }
    }
}
