using Microsoft.AspNetCore.Authorization;
using Sehaty_Plus.Application.Feature.Account.Queries;
using Sehaty_Plus.Application.Feature.Account.Responses;

namespace Sehaty_Plus.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    public class AccountController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("")]
        public async Task<ActionResult<ProfileResponse>> Info()
        {

            var result = await _mediator.Send(new GetProfile(User.GetUserId()!));
            return result.AsActionResult();
        }
        //[HttpPut("info")]
        //public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
        //{
        //    var result = await _mediator.Send(request);
        //    return result.AsActionResult();
        //}
        //[HttpPost("change-email")]
        //public async Task<IActionResult> SendChangeEmailCode(ChangeEmailRequest request)
        //{
        //    var result = await _mediator.Send(request);
        //    return result.AsActionResult();
        //}
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


        //[HttpPost("change-password")]
        //public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        //{
        //    var result = await _mediator.Send(request);
        //    return result.AsActionResult();
        //    }
    }
}
