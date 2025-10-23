using Sehaty_Plus.Application.Feature.Account.Responses;
using Sehaty_Plus.Application.Feature.Auth.Errors;

namespace Sehaty_Plus.Application.Feature.Account.Queries.GetProfile
{
    public record GetProfile(string UserId) : IRequest<Result<ProfileResponse>>;
    public class GetProfileHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetProfile, Result<ProfileResponse>>
    {
        public async Task<Result<ProfileResponse>> Handle(GetProfile request, CancellationToken cancellationToken)
        {

            var response = await _queryExecuter.QueryFirstOrDefault<ProfileResponse>(
                @" SELECT  U.Id, U.FirstName,U.LastName,U.Email,U.phoneNumber,U.Gender,U.ProfilePicture,U.RegisteredDate,IsActive
                       FROM AspNetUsers U WHERE U.Id = @UserId", new { request.UserId });
            if (response is null)
                return Result.Failure<ProfileResponse>(UserErrors.InvalidCredentials);

            return Result.Success(response);
        }
    }
}
