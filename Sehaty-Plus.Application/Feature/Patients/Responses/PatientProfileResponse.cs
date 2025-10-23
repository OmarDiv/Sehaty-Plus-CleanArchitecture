using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Patients.Responses
{
    public record PatientProfileResponse(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        string? ProfilePicture,
        Guid PatientId,
        string NationalId,
        DateTime DateOfBirth,
        string Governorate,
        string City,
        string? BloodType,
        string? EmergencyContact,
        string? MedicalHistory,
        string? Allergies
    );
}
