using FluentValidation;
using projectverseAPI.DTOs.Post;

namespace projectverseAPI.Validators.Post
{
    public class CreatePostCommentRequestDTOValidator : AbstractValidator<CreatePostCommentRequestDTO>
    {
        public CreatePostCommentRequestDTOValidator()
        {
            RuleFor(x => x.Body)
                .NotEmpty()
                .NotNull();
        }
    }
}
