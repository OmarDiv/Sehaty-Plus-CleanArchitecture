using Sehaty_Plus.Application.Common.Const;


namespace Sehaty_Plus.Application.Feature.User.Commands.RegisterPatient
{
    public class RegisterPatientValidator : AbstractValidator<RegisterPatient>
    {
        public RegisterPatientValidator()
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
            RuleFor(x => x.Gender)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(RegexPatterns.EgyptPhoneNumer)
                .WithMessage("Invalid Egyptian phone number. Format: 01xxxxxxxxx or +2001xxxxxxxxx");

            RuleFor(x => x.NationalId)
                .NotEmpty()
                .Length(14)
                .Matches(@"^\d{14}$")
                .WithMessage("National ID must be 14 digits");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .LessThan(DateTime.Today)
                .GreaterThan(DateTime.Today.AddYears(-120))
                .WithMessage("Date of birth must be in the past");

            RuleFor(x => x.BloodType)
                .Matches(@"^(A|B|AB|O)[+-]$")
                .When(x => !string.IsNullOrEmpty(x.BloodType))
                .WithMessage("Please enter a valid blood type (e.g., A+, B-, AB+, O-).");
        }
    }
}
