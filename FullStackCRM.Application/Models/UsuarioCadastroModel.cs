using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Models
{
    public class UsuarioCadastroModel
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? PerfilId { get; set; }
    }
}
