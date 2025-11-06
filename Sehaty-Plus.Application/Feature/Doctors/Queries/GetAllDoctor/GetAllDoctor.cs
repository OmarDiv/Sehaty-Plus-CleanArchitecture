using Sehaty_Plus.Application.Feature.Doctors.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sehaty_Plus.Application.Feature.Doctors.Queries.GetAllDoctor
{
    public record GetAllDoctor() : IRequest<Result<IEnumerable<AdminDoctorListResponse>>>;
    public class GetAllDoctorHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetAllDoctor, Result<IEnumerable<AdminDoctorListResponse>>>
    {
        public async Task<Result<IEnumerable<AdminDoctorListResponse>>> Handle(GetAllDoctor request, CancellationToken cancellationToken)
        {
            var doctors = await _queryExecuter.Query<AdminDoctorListResponse>(@"
                                                SELECT 
                                                    D.Id AS DoctorId,
                                                    U.FirstName,
                                                    U.LastName,
                                                    U.Gender,
                                                    D.LicenseNumber,
                                                    S.Name AS SpecializationName,
                                                    D.YearsOfExperience,
                                                    U.PhoneNumber,
                                                    D.IsVerified
                                                FROM Doctors D
                                                INNER JOIN AspNetUsers U ON D.UserId = U.Id
                                                LEFT JOIN Specializations S ON D.SpecializationId = S.Id
                                                ORDER BY D.YearsOfExperience DESC
                                                ");
            if(doctors is null || !doctors.Any())
            {
                return Result.Failure<IEnumerable<AdminDoctorListResponse>>(new Error("NoDoctorsFound", "No doctors were found in the system.",StatusCodes.Status404NotFound));
            }
            return Result.Success(doctors);
        }
    }
}
