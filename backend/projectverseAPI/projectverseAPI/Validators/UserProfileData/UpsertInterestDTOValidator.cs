using FluentValidation;
using projectverseAPI.DTOs.UserProfileData;

namespace projectverseAPI.Validators.UserProfileData
{
    public class UpsertInterestDTOValidator : AbstractValidator<UpsertInterestDTO>
    {
        public UpsertInterestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
