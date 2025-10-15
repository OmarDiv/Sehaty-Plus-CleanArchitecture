using Sehaty_Plus.Application.Common.Const;


namespace Sehaty_Plus.Application.Feature.Auth.Commands.RegisterDoctor
{
    public class RegisterDoctorValidator : AbstractValidator<RegisterDoctor>
    {
        public RegisterDoctorValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .Matches(RegexPatterns.Password)
                .WithMessage("Password must be at least 8 characters and contain uppercase, lowercase, number and special character");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(RegexPatterns.EgyptPhoneNumer)
                .WithMessage("Invalid Egyptian phone number. Format: 01xxxxxxxxx or +2001xxxxxxxxx");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(x => x.BranchId)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(50);

            RuleFor(x => x.ConsultationFee)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(10000);

            RuleFor(x => x.Education)
                .MaximumLength(500)
                .When(x => !string.IsNullOrEmpty(x.Education));

            RuleFor(x => x.Biography)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Biography));
        }
    }
}
