using FluentValidation;
using Microsoft.Identity.Client;
using Microsoft.OpenApi.Models;
using projectverseAPI.DTOs.Project;

namespace projectverseAPI.Validators.Project
{
    public class CreateProjectRequestDTOValidator : AbstractValidator<CreateProjectRequestDTO>
    {
        public CreateProjectRequestDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(450);

            RuleFor(x => x.ProjectUrl)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.IsPublished)
                .NotNull();

            RuleFor(x => x.IsPrivate)
                .NotNull();

            RuleFor(x => new { x.IsPublished, x.IsPrivate })
                .Custom((pair, context) =>
                {
                    if (pair.IsPublished.Equals(true) && pair.IsPrivate.Equals(true))
                        context.AddFailure("IsPublished", "Project can't be published when private.");
                });

            RuleFor(x => x.UsedTechnologies)
                .NotNull()
                .NotEmpty()
                .Must(x => x.Count > 0)
                .WithMessage("Project must have at least one technology.");
        }
    }
}
