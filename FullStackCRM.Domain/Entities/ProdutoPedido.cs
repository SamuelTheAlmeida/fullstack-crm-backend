using System;

namespace FullStackCRM.Domain.Entities
{
    public class ProdutoPedido
    {
        public Guid? ProdutoId { get; set; }
        public string Nome { get; set; }
        public Guid? PedidoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
    }
}
