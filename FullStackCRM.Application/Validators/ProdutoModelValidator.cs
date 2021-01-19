using FluentValidation;
using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Validators
{
    public class ProdutoModelValidator : AbstractValidator<ProdutoModel>
    {
        public ProdutoModelValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Preco)
                .NotEmpty()
                .NotNull()
                .Must(x => decimal.TryParse(x, out _))
                .WithMessage("Valor inválido para preço do produto");
        }
    }
}
