using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<BaseModel<UsuarioModel>> Autenticar(LoginModel loginModel);
        Task<BaseModel<List<UsuarioModel>>> ListarAsync();
        Task<BaseModel<UsuarioCadastroModel>> InserirAsync(UsuarioCadastroModel usuarioCadastroModel);
        Task<BaseModel<UsuarioCadastroModel>> AtualizarAsync(UsuarioCadastroModel usuarioCadastroModel);
        Task<BaseModel<UsuarioModel>> ObterPorIdAsync(Guid id);
        Task<BaseModel<UsuarioModel>> ExcluirAsync(Guid id);
        BaseModel<List<EnumModel>> ListarPerfis();
    }
}
