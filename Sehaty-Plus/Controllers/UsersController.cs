using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sehaty_Plus.Application.Feature.User.Commands.RegisterDoctor;
using Sehaty_Plus.Application.Feature.User.Commands.RegisterPatient;
using Sehaty_Plus.Application.Feature.User.Commands.RegisterUser;

namespace Sehaty_Plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("register-patient")]
        public async Task<ActionResult<Result>> RegisterPatient([FromBody] RegisterPatient request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPost("register-doctor")]
        public async Task<ActionResult<Result>> RegisterDoctor([FromBody] RegisterDoctor request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPost("register-user")]
        public async Task<ActionResult<Result>> RegisterUser([FromBody] RegisterUser request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
    }
}
