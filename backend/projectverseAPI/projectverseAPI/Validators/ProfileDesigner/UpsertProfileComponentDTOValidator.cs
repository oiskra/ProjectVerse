using FluentValidation;
using projectverseAPI.DTOs.Designer;

namespace projectverseAPI.Validators.ProfileDesigner
{
    public class UpsertProfileComponentDTOValidator : AbstractValidator<UpsertProfileComponentDTO>
    {
        public UpsertProfileComponentDTOValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ComponentType)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ColStart)
                .NotEmpty()
                .NotNull()
                .InclusiveBetween(1,12)
                .NotEqual(x => x.ColEnd)
                .Must((obj, n) => n < obj.ColEnd);
            
            RuleFor(x => x.ColEnd)
                .NotEmpty()
                .NotNull()
                .NotEqual(x => x.ColStart)
                .InclusiveBetween(2, 13)
                .Must((obj, n) => n > obj.ColStart);

            RuleFor(x => x.RowStart)
                .NotEmpty()
                .NotNull()
                .NotEqual(x => x.RowEnd)
                .InclusiveBetween(1,11)
                .Must((obj, n) => n < obj.RowEnd);

            RuleFor(x => x.RowEnd)
                .NotEmpty()
                .NotNull()
                .NotEqual(x => x.RowStart)
                .InclusiveBetween(2,12)
                .Must((obj, n) => n > obj.RowStart);



        }
    }
}
