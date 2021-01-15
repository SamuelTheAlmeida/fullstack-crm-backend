using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioModel Autenticar(string usuario, string senha);
    }
}
