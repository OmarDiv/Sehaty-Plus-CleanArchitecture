using Sehaty_Plus.Application.Feature.Auth.Errors;

namespace Sehaty_Plus.Application.Feature.Patients.Queries.GetPatientProfile
{
    public record GetPatientProfile(string UserId) : IRequest<Result<PatientProfileResponse>>;
    public class GetPatientProfileHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetPatientProfile, Result<PatientProfileResponse>>
    {
        public async Task<Result<PatientProfileResponse>> Handle(GetPatientProfile request, CancellationToken cancellationToken)
        {
            var response = await _queryExecuter.QueryFirstOrDefault<PatientProfileResponse>(
                @" SELECT  U.Id, U.FirstName,U.LastName,U.Email,U.PhoneNumber,U.Gender,U.ProfilePicture,P.Id AS PatientId,P.NationalId,
                               P.DateOfBirth, P.Governorate,P.City, P.BloodType,P.EmergencyContact, P.MedicalHistory, P.Allergies
                               FROM Patients P INNER JOIN AspNetUsers U ON P.UserId = U.Id WHERE P.UserId = @UserId", new { request.UserId });

            if (response is null)
                return Result.Failure<PatientProfileResponse>(UserErrors.InvalidCredentials);
            return Result.Success(response);

        }
    }
}
