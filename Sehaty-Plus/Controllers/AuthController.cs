using Microsoft.AspNetCore.RateLimiting;
using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.ConfirmResetPassword;
using Sehaty_Plus.Application.Feature.Auth.Commands.GetRefreshToken;
using Sehaty_Plus.Application.Feature.Auth.Commands.Login;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterAdmin;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterDoctor;
using Sehaty_Plus.Application.Feature.Auth.Commands.RegisterPatient;
using Sehaty_Plus.Application.Feature.Auth.Commands.ResendConfirmEmail;
using Sehaty_Plus.Application.Feature.Auth.Commands.RevokeRefreshToken;
using Sehaty_Plus.Application.Feature.Auth.Commands.SendForgetPasswordOtp;
using Sehaty_Plus.Application.Feature.Auth.Commands.VerfiyForgetPasswordOtp;
using Sehaty_Plus.Application.Feature.Auth.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("Auth")]
    [ApiController]
    [EnableRateLimiting("ipLimit")]
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
        [HttpPost("register/patient")]
        public async Task<ActionResult<Result>> RegisterPatient([FromBody] RegisterPatient request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPost("register/doctor")]
        public async Task<ActionResult<Result>> RegisterDoctor([FromBody] RegisterDoctor request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPost("register/Admin")]
        public async Task<ActionResult<Result>> RegisterAdmin([FromBody] RegisterAdmin request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPost("confirm-email")]
        public async Task<ActionResult<Result>> ConfirmEmail([FromBody] ConfirmEmail request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }
        [HttpPost("resend-confirm-email")]
        public async Task<ActionResult<Result>> ResendConfirmEmail([FromBody] ResendConfirmEmail request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.AsNoContentResult();
        }

        [HttpPost("send-forgetpassword-otp")]
        public async Task<ActionResult<string>> SendForgetPasswordOtp([FromBody] ForgotPassword forgotPassword)
        {
            var result = await _mediator.Send(forgotPassword);
            return result.AsActionResult();
        }
        [HttpPost("Verfiy-forgetpassword-otp")]
        public async Task<ActionResult<string>> VerfiyForgetPasswordOtp([FromBody] VerfiyForgetPasswordOtp verfiyForgetPasswordOtp)
        {
            var result = await _mediator.Send(verfiyForgetPasswordOtp);
            return result.AsActionResult();
        }
        [HttpPost("confirm-reset-password")]
        public async Task<ActionResult<string>> ConfirmResetPassword([FromBody] ConfirmResetPassword confirmResetPassword)
        {
            var result = await _mediator.Send(confirmResetPassword);
            return result.AsNoContentResult();
        }

    }
}
