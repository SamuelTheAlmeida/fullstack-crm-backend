using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FullStackCRM.Application.Models
{
    public class ProdutoModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Preco { get; set; }
    }
}
