using FluentValidation;
using projectverseAPI.DTOs.UserProfileData;

namespace projectverseAPI.Validators.UserProfileData
{
    public class UpsertSocialMediaDTOValidator : AbstractValidator<UpsertSocialMediaDTO>
    {
        public UpsertSocialMediaDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Link)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");

        }
    }
}
