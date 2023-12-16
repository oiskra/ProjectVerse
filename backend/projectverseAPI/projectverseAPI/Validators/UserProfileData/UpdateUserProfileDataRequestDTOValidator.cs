using FluentValidation;
using projectverseAPI.DTOs.UserProfileData;

namespace projectverseAPI.Validators.UserProfile
{
    public class UpdateUserProfileDataRequestDTOValidator : AbstractValidator<UpdateUserProfileDataRequestDTO>
    {
        public UpdateUserProfileDataRequestDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.AboutMe)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Achievements)
                    .NotNull()
                    .NotEmpty();

            RuleFor(x => x.PrimaryTechnology)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.KnownTechnologies)
                .NotNull();

            RuleFor(x => x.Interests)
                .NotNull();
            
            RuleFor(x => x.Socials)
                .NotNull();

            RuleFor(x => x.Certificates)
                .NotNull();

            RuleFor(x => x.Educations)
                .NotNull();
        }
    }
}
