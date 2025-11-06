using Microsoft.AspNetCore.Authorization;
using Sehaty_Plus.Application.Feature.Account.Commands.ChangeEmail;
using Sehaty_Plus.Application.Feature.Account.Commands.ChangePassword;
using Sehaty_Plus.Application.Feature.Account.Commands.UpdateProfile;
using Sehaty_Plus.Application.Feature.Account.Queries.GetProfile;
using Sehaty_Plus.Application.Feature.Account.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    public class AccountController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("")]
        public async Task<ActionResult<ProfileResponse>> GetProfile()
        {
            var result = await _mediator.Send(new GetProfile(User.GetUserId()!));
            return result.AsActionResult();
        }

        [HttpPut("")]
        public async Task<ActionResult<Result>> UpdateProfile([FromBody] UpdateProfileDto request)
        {
            var result = await _mediator.Send(new UpdateProfile(User.GetUserId()!, request.FirstName, request.LastName, request.Gender));
            return result.AsNoContentResult();
        }
        [HttpPost("change-email")]
        public async Task<ActionResult<Result>> SendChangeEmailCode()
        {
            var result = await _mediator.Send(new ChangeEmail(User.GetUserId()!));
            return result.AsNoContentResult();
        }
        //[HttpPost("confirm-change-email")]
        //public async Task<IActionResult> ConfirmChangeEmail(ConfirmChangeEmailRequest request)
        //{
        //    var result = await _mediator.Send(request);
        //    return result.AsActionResult();
        //}
        //[HttpPost("resend-confirm-change-email")]
        //public async Task<IActionResult> ResndConfirmChangeEmail(ReSendChangeEmailCode request)
        //{
        //    var result = await _mediator.Send(request);
        //    return result.AsActionResult();
        //}
        [HttpPost("change-password")]
        public async Task<ActionResult<Result>> ChangePassword([FromBody] ChangePasswordDto request)
        {
            var result = await _mediator.Send(new ChangePassword(User.GetUserId()!,
             request.OldPassword, request.NewPassword, request.ConfirmPassword));
            return result.AsNoContentResult();
        }
    }
}
