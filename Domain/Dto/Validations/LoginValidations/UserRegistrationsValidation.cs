using Domain.Dto.LoginDtos;
using FluentValidation;

namespace Domain.Dto.Validations.LoginValidations
{
    public class UserRegistrationsValidation : AbstractValidator<UserRegistrationsDto>
    {
        public UserRegistrationsValidation()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format!");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is Required")
                .MinimumLength(6).WithMessage("Password length must be at least 6 characters")
                .Must(password =>
                    password.Any(char.IsUpper) &&
                    password.Any(char.IsDigit) &&
                    password.Any(c => char.IsSymbol(c) || char.IsPunctuation(c)))
                .WithMessage("Password must contain at least one uppercase letter, one special character, and one number");

            RuleFor(x => x.role).NotEmpty().WithMessage("Role of user must be specified");
        }

    }
}
