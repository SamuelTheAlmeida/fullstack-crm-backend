using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioModel> Autenticar(LoginModel loginModel);
        Task<BaseModel<List<UsuarioModel>>> ListarAsync();
        Task<BaseModel<UsuarioModel>> InserirAsync(UsuarioModel usuarioModel);
        Task<BaseModel<UsuarioModel>> AtualizarAsync(UsuarioModel usuarioModel);
        Task<BaseModel<UsuarioModel>> ObterPorIdAsync(Guid id);
        Task<BaseModel<UsuarioModel>> ExcluirAsync(Guid id);
    }
}
