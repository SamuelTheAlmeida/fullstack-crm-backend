using FullStackCRM.Application.Models;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UsuarioModel user);
    }
}
