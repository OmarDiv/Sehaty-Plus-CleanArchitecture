using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Patients.Responses
{
    public record AdminPatientDetailsResponse(
    Guid PatientId,
    string FirstName,
    string LastName,
    string Email,
  [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
    string PhoneNumber,
    string NationalId,
    string? Governorate,
    string? City,
    string? BloodType,
    DateTime DateOfBirth,
    DateTime RegisteredDate,
    bool IsActive);

}
