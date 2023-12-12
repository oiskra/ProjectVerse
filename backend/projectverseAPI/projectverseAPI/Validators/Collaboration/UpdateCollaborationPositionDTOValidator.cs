using FluentValidation;
using projectverseAPI.DTOs.Collaboration;

namespace projectverseAPI.Validators.Collaboration
{
    public class UpdateCollaborationPositionDTOValidator : AbstractValidator<UpdateCollaborationPositionDTO>
    {
        public UpdateCollaborationPositionDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Description)
               .NotEmpty()
               .NotNull();
        }
    }
    }
}
