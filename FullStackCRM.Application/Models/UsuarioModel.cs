using System;

namespace FullStackCRM.Application.Models
{
    public class UsuarioModel
    {
        public Guid? Id { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
    }
}
