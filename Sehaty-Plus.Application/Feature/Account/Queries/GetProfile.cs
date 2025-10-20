using Sehaty_Plus.Application.Feature.Account.Responses;
namespace Sehaty_Plus.Application.Feature.Account.Queries
{
    public record GetProfile(string UserId) : IRequest<Result<ProfileResponse>>;
    public class GetProfileHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetProfile, Result<ProfileResponse>>
    {
        public async Task<Result<ProfileResponse>> Handle(GetProfile request, CancellationToken cancellationToken)
        {
            var response = await _queryExecuter.QueryFirstOrDefault<ProfileResponse>(
                @"Select Id, FirstName, LastName, Email,PhoneNumber,Gender,ProfilePicture From AspNetUsers where Id = @UserId",
                new { request.UserId });
            if (response == null)
                return Result.Failure<ProfileResponse>(new(
                    "Profile.NotFound",
                    "User profile not found.",
                    StatusCode: StatusCodes.Status404NotFound
                    ));
            return Result.Success(response);
        }
    }
}
