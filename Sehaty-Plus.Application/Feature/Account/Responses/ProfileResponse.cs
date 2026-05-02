using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Account.Responses
{
    public record ProfileResponse(
        long Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        Gender Gender,
        string? ProfilePicture,
        DateOnly RegisteredDate,
        bool IsActive
    );
}