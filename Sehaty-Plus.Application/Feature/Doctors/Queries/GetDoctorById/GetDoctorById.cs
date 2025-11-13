using Sehaty_Plus.Application.Feature.Auth.Errors;
using Sehaty_Plus.Application.Feature.Doctors.Responses;
namespace Sehaty_Plus.Application.Feature.Doctors.Queries.GetDoctorById
{
    public record GetDoctorById(string DoctorId) : IRequest<Result<AdminDoctorDetailsResponse>>;
    public class GetDocrorByIdHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetDoctorById, Result<AdminDoctorDetailsResponse>>
    {
        public async Task<Result<AdminDoctorDetailsResponse>> Handle(GetDoctorById request, CancellationToken cancellationToken)
        {
            var result = await _queryExecuter.QueryFirstOrDefault<AdminDoctorDetailsResponse>(
                                                    @"
                                            SELECT 
                                                d.Id AS DoctorId,
                                                u.FirstName,
                                                u.LastName,
                                                u.Gender,
                                                d.LicenseNumber,
                                                s.Name AS SpecializationName,
                                                d.YearsOfExperience,
                                                d.Education,
                                                d.Biography,
                                                u.PhoneNumber,
                                                d.IsVerified,
                                                u.RegisteredDate,
                                                u.ProfilePicture
                                            FROM Doctors d
                                            INNER JOIN AspNetUsers u ON d.UserId = u.Id
                                            INNER JOIN Specializations s ON d.SpecializationId = s.Id
                                            WHERE d.Id = @DoctorId"
                                                    , new { request.DoctorId }
                                                );

            if (result is null)
                return Result.Failure<AdminDoctorDetailsResponse>(UserErrors.InvalidCredentials);
            return Result.Success(result);
        }
    }
}
