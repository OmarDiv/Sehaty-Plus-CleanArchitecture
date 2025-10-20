using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Application.Feature.Account.Responses
{
    public record ProfileResponse( 
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string? ProfilePicture);
}