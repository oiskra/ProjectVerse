using FluentValidation;
using projectverseAPI.DTOs.UserProfileData;

namespace projectverseAPI.Validators.UserProfileData
{
    public class UpsertCeritficateDTOValidator : AbstractValidator<UpsertCertificateDTO>
    {
        public UpsertCeritficateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Institution)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.IssuedAt)
                .NotNull()
                .NotEmpty()
                .Must(d => d.GetType().Equals(typeof(DateTime)))
                .LessThan(d => d.ExpiresAt);

            RuleFor(x => x.ExpiresAt)
                .NotNull()
                .NotEmpty()
                .Must(d => d.GetType().Equals(typeof(DateTime)))
                .GreaterThan(d => d.IssuedAt);
        }
    }
}
