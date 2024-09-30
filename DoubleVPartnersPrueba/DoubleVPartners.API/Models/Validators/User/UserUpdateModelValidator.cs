using FluentValidation;

namespace DoubleVPartners.API.Models.Validators.User
{
    public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
    {
        public UserUpdateModelValidator()
        {
            RuleFor(x => x.IdUser)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.IdRole)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull()
                .MaximumLength(15);

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(500);
        }
    }
}
