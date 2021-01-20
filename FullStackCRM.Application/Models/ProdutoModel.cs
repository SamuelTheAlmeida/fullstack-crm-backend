using System;
using System.ComponentModel.DataAnnotations;

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
