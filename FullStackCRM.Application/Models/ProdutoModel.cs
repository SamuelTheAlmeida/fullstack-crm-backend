using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Models
{
    public class ProdutoModel
    {
        public Guid? Id { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }
    }
}
