using Domain.Dto.LoginDtos;
using FluentValidation;

namespace Domain.Dto.Validations.LoginValidations
{
    public class ResetPasswordValidation : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidation()
        {
            RuleFor(x => x.Otp).NotEmpty().WithMessage("Otp must contain some value")
                .Length(6).WithMessage("Otp must be exactly 6 characters long")
                .Matches("^[0-9]{6}$").WithMessage("Otp must be exactly 6 digits");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password length must be at least 6 characters")
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsDigit) &&
                    password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
                .WithMessage("Password must contain at least one uppercase letter, one special character, and one number");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password length must be at least 6 characters")
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match")
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsDigit) &&
                    password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
                .WithMessage("Password must contain at least one uppercase letter, one special character, and one number");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Invalid Email Format");
        }
    }
}
