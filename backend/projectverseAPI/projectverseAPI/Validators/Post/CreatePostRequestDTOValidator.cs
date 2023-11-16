using FluentValidation;
using projectverseAPI.DTOs.Post;

namespace projectverseAPI.Validators.Post
{
    public class CreatePostRequestDTOValidator : AbstractValidator<CreatePostRequestDTO>
    {
        public CreatePostRequestDTOValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotEmpty()
                .NotNull();
        }
    }
}
