using Sehaty_Plus.Application.Feature.Auth.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Patients.Queries.GetPatientById
{
    public record GetPatientById(Guid PatientId) : IRequest<Result<AdminPatientDetailsResponse>>;
    public class GetPatientByIdHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetPatientById, Result<AdminPatientDetailsResponse>>
    {
        public async Task<Result<AdminPatientDetailsResponse>> Handle(GetPatientById request, CancellationToken cancellationToken)
        {
            var patient = await _queryExecuter.QueryFirstOrDefault<AdminPatientDetailsResponse>(@"
                                                    SELECT  
                                                        P.Id AS PatientId,
                                                        U.FirstName,
                                                        U.LastName,
                                                        U.Email,
                                                        U.Gender,
                                                        U.PhoneNumber,
                                                        P.NationalId,
                                                        P.Governorate,
                                                        P.City,
                                                        P.BloodType,
                                                        P.DateOfBirth,
                                                        U.RegisteredDate,
                                                        U.IsActive
                                                    FROM Patients P
                                                    INNER JOIN AspNetUsers U ON P.UserId = U.Id
                                                    WHERE P.Id = @PatientId", new { request.PatientId });
            if(patient is null)
                return Result.Failure<AdminPatientDetailsResponse>(UserErrors.InvalidCredentials);
            return Result.Success(patient);

        }
    }
}
