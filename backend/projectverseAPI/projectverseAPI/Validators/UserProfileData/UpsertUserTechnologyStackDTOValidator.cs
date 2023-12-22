using FluentValidation;
using projectverseAPI.DTOs.UserProfileData;

namespace projectverseAPI.Validators.UserProfileData
{
    public class UpsertUserTechnologyStackDTOValidator : AbstractValidator<UpsertUserTechnologyStackDTO>
    {
        public UpsertUserTechnologyStackDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Level)
                .IsInEnum()
                .NotNull()
                .NotEmpty();
        }
    }
}
