using FullStackCRM.Domain.Entities;
using System.Threading.Tasks;

namespace FullStackCRM.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AutenticarAsync(string usuario, string senha);
    }
}
