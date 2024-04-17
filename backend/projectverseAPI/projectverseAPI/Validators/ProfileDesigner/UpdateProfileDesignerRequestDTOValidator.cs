using FluentValidation;
using projectverseAPI.DTOs.Designer;

namespace projectverseAPI.Validators.ProfileDesigner
{
    public class UpdateProfileDesignerRequestDTOValidator : AbstractValidator<UpdateProfileDesignerRequestDTO>
    {
        public UpdateProfileDesignerRequestDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Theme)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Components)
                .NotEmpty()
                .NotNull()
                .Must(comps => comps.Any(comp => comp.ComponentType.Category == "Header"))
                .WithMessage("Profile must have a Header component.");
        }
    }
}
