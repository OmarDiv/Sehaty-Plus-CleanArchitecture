using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.RegisterDoctor
{
    public record RegisterDoctor(
        string Email,
        string Password,
        string ConfirmPassword,
        string FirstName,
        string LastName,
        string PhoneNumber,
        Gender Gender,
        string LicenseNumber,
        int YearsOfExperience,
        string Education,
        string? Biography,
        decimal ConsultationFee,
        int ClinicId) : IRequest<Result>;
    public class RegisterDoctorHandler(IAuthService _authService) : IRequestHandler<RegisterDoctor, Result>
    {
        public async Task<Result> Handle(RegisterDoctor request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterDoctorAsync(request, cancellationToken);
        }
    }
}
