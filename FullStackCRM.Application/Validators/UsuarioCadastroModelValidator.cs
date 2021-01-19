using FluentValidation;
using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Validators
{
    public class UsuarioCadastroModelValidator : AbstractValidator<UsuarioCadastroModel>
    {
        public UsuarioCadastroModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress();

            RuleFor(x => x.PerfilId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Senha)
                .NotEmpty()
                .NotNull()
                .Length(32)
                .WithMessage("Senha em formato inválido.");
               
        }
    }
}
