using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ste.Framework.Common;
using Ste.User.Application.Security;

namespace Ste.User.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SecurityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<OkObjectResult> Login(Login.Request request)
        {
            var r = await _mediator.Send(request);
            return Ok(r);
        }

        [HttpGet("[action]")]
        public async Task<OkObjectResult> Logout([FromHeader(Name = "token")][Required] string token, [FromQuery][Required] Logout.Request request)
        {
            var r = await _mediator.Send(request);
            return Ok(r);
        }
    }
}