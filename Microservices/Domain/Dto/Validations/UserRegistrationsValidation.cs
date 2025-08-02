using FluentValidation;
using Libraries.Models;

namespace Domain.Dto.Validations
{
    public class UserRegistrationsValidation : AbstractValidator<UserRegistrationsDto>
    {
        public UserRegistrationsValidation()
        {

            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format!");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password length must be at least 6 characters");

            RuleFor(x => x.role).NotEmpty().WithMessage("Role of user must be specified");
        }

    }
}
