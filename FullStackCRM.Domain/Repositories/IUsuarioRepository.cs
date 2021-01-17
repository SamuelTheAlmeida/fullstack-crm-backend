using FullStackCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackCRM.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AutenticarAsync(string usuario, string senha);
        Task<List<Usuario>> ListarAsync();
        Task<Usuario> InserirAsync(Usuario usuario);
        Task<Usuario> AtualizarAsync(Usuario usuario);
        Task<Usuario> ObterPorIdAsync(Guid id);
        Task ExcluirAsync(Guid id);
    }
}
