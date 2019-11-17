using Microsoft.AspNetCore.Mvc;
using SignalRChat.Domain.Results;
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
        [Route("is-alive")]
        public ActionResult IsAlive()
        {
            return Ok(true);
        }

        protected IActionResult HandleResult<TSuccess, TFailure>(Result<TSuccess, TFailure> result) where TFailure : Error
        {
            if (result.IsFailure)
                return HandleFailure(result.Failure);

            return Ok(result.Success);
        }   

        protected IActionResult HandleFailure(Error error)
        {
            return StatusCode(error.ErrorCode.GetHashCode(), error.Message);
        }
    }
}
