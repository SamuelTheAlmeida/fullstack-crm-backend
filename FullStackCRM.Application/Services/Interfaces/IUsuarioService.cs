using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> Autenticar(string usuario, string senha);
    }
}
