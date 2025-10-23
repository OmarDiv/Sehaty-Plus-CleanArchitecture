namespace Sehaty_Plus.Application.Feature.Patients.Queries.GetAllPatients
{
    public record GetAllPatients() : IRequest<Result<IEnumerable<AdminPatientListResponse>>>;
    public class GetAllPatientsHandler(IQueryExecuter _queryExecuter) : IRequestHandler<GetAllPatients, Result<IEnumerable<AdminPatientListResponse>>>
    {
        public async Task<Result<IEnumerable<AdminPatientListResponse>>> Handle(GetAllPatients request, CancellationToken cancellationToken)
        {
            var patients = await _queryExecuter.Query<AdminPatientListResponse>(@"
             SELECT 
                  P.Id AS PatientId,
                  U.FirstName,
                  U.LastName,
                  U.Gender,
                  P.BloodType,
                  P.Governorate,
                  P.City,
                  U.PhoneNumber
                 FROM Patients P
                  INNER JOIN AspNetUsers U ON P.UserId = U.Id");

            return Result.Success(patients);
        }
    }
}
