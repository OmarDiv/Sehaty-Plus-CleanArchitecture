using Sehaty_Plus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Patients.Responses
{
    public record AdminPatientListResponse(
        Guid PatientId,
        string FirstName,
        string LastName,
        [property: JsonConverter(typeof(JsonStringEnumConverter))] Gender Gender,
        string? BloodType,
        string? Governorate,
        string? City,
        string PhoneNumber
    );

}
