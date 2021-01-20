using FluentValidation;
using FullStackCRM.Application.Models;
using System.Linq;

namespace FullStackCRM.Application.Validators
{
    public class PedidoModelValidator : AbstractValidator<PedidoModel>
    {
        public PedidoModelValidator()
        {
            RuleFor(x => x.EmailComprador)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.ProdutosPedido)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Valor)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Valor)
                .Equal(x => x
                    .ProdutosPedido
                        .Sum(x => x.PrecoTotal));

            RuleForEach(x => x.ProdutosPedido)
                .SetValidator(x => new ProdutoPedidoModelValidator());
        }
    }
}
