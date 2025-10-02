using Sehaty_Plus.Application.Feature.Auth.Commands.GetRefreshToken;
using Sehaty_Plus.Application.Feature.Auth.Commands.Login;
using Sehaty_Plus.Application.Feature.Auth.Commands.RevokeRefreshToken;
using Sehaty_Plus.Application.Feature.Auth.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("Auth")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginUser request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();

        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] GetRefrshToken request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsActionResult();
        }

        [HttpPost("revoke")]
        public async Task<ActionResult<AuthResponse>> RevokeRefreshToken([FromBody] RevokeRefreshToken request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }

        //[HttpPost("register")]
        //public async Task<ActionResult> Register()
        //{
        //    return Ok("Register endpoint");
        //}
    }
}
