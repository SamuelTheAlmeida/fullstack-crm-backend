using System;
using System.Collections.Generic;

namespace FullStackCRM.Domain.Entities
{
    public class Pedido
    {
        public Guid? Id { get; set; }
        public string EmailComprador { get; set; }
        public decimal Valor { get; set; }
        public List<ProdutoPedido> ProdutosPedido { get; set; }
    }
}
