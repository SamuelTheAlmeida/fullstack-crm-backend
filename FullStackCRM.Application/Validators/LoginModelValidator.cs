using FluentValidation;
using FullStackCRM.Application.Models;

namespace FullStackCRM.Application.Validators
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Email).NotNull().EmailAddress();
            RuleFor(x => x.Senha).Length(32);
        }
    }
}
