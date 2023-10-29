using FluentValidation;
using projectverseAPI.DTOs.Authentication;

namespace projectverseAPI.Validators.Authentication
{
    public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Required");
        }
    }
}
