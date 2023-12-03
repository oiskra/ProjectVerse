using FluentValidation;
using projectverseAPI.DTOs.Collaboration;

namespace projectverseAPI.Validators.Collaboration
{
    public class CreateCollaborationPositionDTOValidator : AbstractValidator<CreateCollaborationPositionDTO>
    {
        public CreateCollaborationPositionDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull();
            
            RuleFor(x => x.Description)
               .NotEmpty()
               .NotNull();
        }
    }
}
