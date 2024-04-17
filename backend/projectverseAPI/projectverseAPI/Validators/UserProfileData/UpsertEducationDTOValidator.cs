using FluentValidation;
using projectverseAPI.DTOs.UserProfileData;

namespace projectverseAPI.Validators.UserProfileData
{
    public class UpsertEducationDTOValidator : AbstractValidator<UpsertEducationDTO>
    {
        public UpsertEducationDTOValidator()
        {
            RuleFor(x => x.University)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Department)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Course)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.Major)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.AcademicTitle)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.StartDate)
                .NotNull()
                .NotEmpty()
                .Must(d => d.GetType().Equals(typeof(DateTime)))
                .LessThan(d => d.EndDate); 

            RuleFor(x => x.EndDate)
                .NotNull()
                .NotEmpty()
                .Must(d => d.GetType().Equals(typeof(DateTime)))
                .GreaterThan(d => d.StartDate);
        }
    }
}
