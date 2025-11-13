using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Doctors.Responses
{
    public record AdminDoctorDetailsResponse(
        string DoctorId,
        string FirstName,
        string LastName,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        string LicenseNumber,
        string SpecializationName,
        int YearsOfExperience,
        string? Education,
        string? Biography,
        string PhoneNumber,
        bool IsVerified,
        DateOnly RegisteredDate,
        string? ProfilePicture
    );
}