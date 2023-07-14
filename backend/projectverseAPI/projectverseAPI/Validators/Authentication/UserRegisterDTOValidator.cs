using FluentValidation;
using projectverseAPI.DTOs.Authentication;

namespace projectverseAPI.Validators.Authentication
{
    public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Required")
                .MinimumLength(8).WithMessage("Minimum length is 8 characters")
                .Matches("[A-Z]").WithMessage("Require uppercase letter")
                .Matches("[a-z]").WithMessage("Require lowercase letter")
                .Matches("[0-9]").WithMessage("Require digit");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Required")
                .Equal(x => x.Password).WithMessage("Must be the same as password");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Required");
        }
    }
}
