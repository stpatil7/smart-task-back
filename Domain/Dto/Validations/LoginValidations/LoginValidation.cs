using Domain.Dto.LoginDtos;
using FluentValidation;

namespace Domain.Dto.Validations.LoginValidations
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Invalid Email Format");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password length must be at least 6 characters")
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsDigit) &&
                    password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
                .WithMessage("Password must contain at least one uppercase letter, one special character, and one number");
        }
    }
}
