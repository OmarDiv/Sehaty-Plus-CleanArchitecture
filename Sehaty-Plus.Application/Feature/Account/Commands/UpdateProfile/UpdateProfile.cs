using Microsoft.AspNetCore.Identity;
using Sehaty_Plus.Application.Feature.Auth.Errors;
using Sehaty_Plus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Account.Commands.UpdateProfile
{
    public record UpdateProfileDto(string FirstName, string LastName, Gender Gender);
    public record UpdateProfile(string UserId, string FirstName, string LastName, Gender Gender) : IRequest<Result>;
    public class UpdateProfileHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<UpdateProfile, Result>
    {
        public async Task<Result> Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvalidCredentials);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Gender = request.Gender;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var error = result.Errors.First();
                return Result.Failure(new(error.Code, error.Description, StatusCodes.Status400BadRequest));
            }
            return Result.Success();
        }
    }
}
