using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Models
{
    public class ProdutoPedidoModel
    {
        public Guid? ProdutoId { get; set; }
        public string Nome { get; set; }
        public Guid? PedidoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
    }
}
