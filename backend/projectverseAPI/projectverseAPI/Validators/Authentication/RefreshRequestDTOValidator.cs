using FluentValidation;
using projectverseAPI.DTOs.Authentication;

namespace projectverseAPI.Validators.Authentication
{
    public class RefreshRequestDTOValidator : AbstractValidator<RefreshRequestDTO>
    {
        public RefreshRequestDTOValidator()
        {
            RuleFor(x => x.AccessToken)
                .NotEmpty().WithMessage("Required");

            RuleFor(x => x.RefreshToken)
                .NotEmpty().WithMessage("Required");
        }
    }
}
