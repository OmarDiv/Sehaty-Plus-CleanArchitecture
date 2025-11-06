using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Doctors.Responses
{
    public record AdminDoctorListResponse(
        Guid DoctorId,
        string FirstName,
        string LastName,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        string LicenseNumber,
        string SpecializationName,
        int YearsOfExperience,
        string PhoneNumber,
        bool IsVerified
    );
}
