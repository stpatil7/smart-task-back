using Domain.Dto.ProjectCreateDtos;
using FluentValidation;

namespace Domain.Dto.Validations.ProjectValidations
{
    public class ProjectCreateRequestValidations : AbstractValidator<ProjectCreateRequestDto>
    {
        public ProjectCreateRequestValidations()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(150).WithMessage("Should not exceed 150 characters");

            RuleFor(x => x.StartDate).NotEmpty().WithMessage("Start date is required")
                .Must(date => date.Date >= DateTime.Today)
                .WithMessage("Start date must be today or later");

            RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date is required")
                .Must(date => date.Date >= DateTime.Today)
                .WithMessage("End date must be today or later");

            RuleFor(x => x.CreatedById).NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than zero");
        }
    }
}
