using System;
using System.Collections.Generic;

namespace FullStackCRM.Application.Models
{
    public class PedidoModel
    {
        public Guid? Id { get; set; }
        public string EmailComprador { get; set; }
        public decimal Valor { get; set; }
        public List<ProdutoPedidoModel> ProdutosPedido { get; set; }
    }
}
