using Sehaty_Plus.Domain.Enums;
using System.Text.Json.Serialization;

namespace Sehaty_Plus.Application.Feature.Patients.Responses
{
    public record AdminPatientListResponse(
        string PatientId,
        string FirstName,
        string LastName,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        string? BloodType,
        string? Governorate,
        string? City,
        string PhoneNumber
    );

}
