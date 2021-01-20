using FluentValidation;
using FullStackCRM.Application.Models;

namespace FullStackCRM.Application.Validators
{
    public class ProdutoPedidoModelValidator : AbstractValidator<ProdutoPedidoModel>
    {
        public ProdutoPedidoModelValidator()
        {
            RuleFor(x => x.ProdutoId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Quantidade)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PrecoUnitario)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PrecoTotal)
                .NotNull()
                .NotEmpty()
                .Equal(x => x.PrecoUnitario * x.Quantidade);
        }
    }
}
