using Microsoft.AspNetCore.Identity;
using Sehaty_Plus.Application.Feature.Auth.Errors;

namespace Sehaty_Plus.Application.Feature.Account.Commands.ChangePassword
{
    public record ChangePasswordDto(string OldPassword, string NewPassword, string ConfirmPassword);
    public record ChangePassword(string UserId, string OldPassword, string NewPassword, string ConfirmPassword) : IRequest<Result>;
    public class ChangePasswordHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<ChangePassword, Result>
    {
        public async Task<Result> Handle(ChangePassword request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();

                return Result.Failure<Result>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }
            return Result.Success();
        }
    }
}
