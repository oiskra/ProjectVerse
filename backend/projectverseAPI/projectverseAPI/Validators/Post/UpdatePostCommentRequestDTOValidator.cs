using FluentValidation;
using projectverseAPI.DTOs.Post;

namespace projectverseAPI.Validators.Post
{
    public class UpdatePostCommentRequestDTOValidator : AbstractValidator<UpdatePostCommentRequestDTO>
    {
        public UpdatePostCommentRequestDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Body)
                .NotNull()
                .NotEmpty();

        }
    }
}
