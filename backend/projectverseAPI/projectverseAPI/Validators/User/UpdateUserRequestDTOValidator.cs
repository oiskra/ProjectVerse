using FluentValidation;
using projectverseAPI.DTOs.User;

namespace projectverseAPI.Validators.User
{
    public class UpdateUserRequestDTOValidator : AbstractValidator<UpdateUserRequestDTO>
    {
        public UpdateUserRequestDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty();
        }
    }
}
