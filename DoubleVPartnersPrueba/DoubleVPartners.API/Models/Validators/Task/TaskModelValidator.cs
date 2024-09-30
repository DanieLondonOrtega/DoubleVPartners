using FluentValidation;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DoubleVPartners.API.Models.Validators.Task
{
    public class TaskModelValidator : AbstractValidator<TaskModel>
    {
        public TaskModelValidator()
        {

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
