using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Account.Responses
{
    public record ProfileResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        string? ProfilePicture,
        DateOnly RegisteredDate,
        bool IsActive
        );
}