using FluentValidation;

namespace DoubleVPartners.API.Models.Validators.Task
{
    public class TaskUpdateModelValidator : AbstractValidator<TaskUpdateModel>
    {
        public TaskUpdateModelValidator()
        {
            RuleFor(x => x.IdTask)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.StatusTask)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.NameTask)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.CreateDate)
                .NotEmpty()
                .NotNull();
        }
    }
}
