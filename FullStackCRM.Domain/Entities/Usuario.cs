using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Domain.Entities
{
    public class Usuario
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public EPerfil Perfil { get; set; }
    }
}
