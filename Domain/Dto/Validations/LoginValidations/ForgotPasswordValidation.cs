using Domain.Dto.LoginDtos;
using FluentValidation;

namespace Domain.Dto.Validations.LoginValidations
{
    public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required")
                .EmailAddress().WithMessage("Invalid Email Format");
        }
    }
}
