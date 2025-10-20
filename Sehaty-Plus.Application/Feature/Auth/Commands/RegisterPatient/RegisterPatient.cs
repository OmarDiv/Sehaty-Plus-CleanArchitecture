using Sehaty_Plus.Domain.Enums;

namespace Sehaty_Plus.Application.Feature.Auth.Commands.RegisterPatient
{
    public record RegisterPatient(
        string Email,
        string Password,
        string ConfirmPassword,
        string FirstName,
        string LastName,
        string PhoneNumber,
        Gender Gender,
        string NationalId,
        DateTime DateOfBirth,
        string? BloodType,
        string? EmergencyContact,
        string? Allergies
        ) : IRequest<Result>;
    public class RegisterPatientHandler(IAuthService _authService) : IRequestHandler<RegisterPatient, Result>
    {

        public async Task<Result> Handle(RegisterPatient request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterPatientAsync(request, cancellationToken);
        }
    }
}
