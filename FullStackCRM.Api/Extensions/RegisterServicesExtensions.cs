using FullStackCRM.Application.Services;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FullStackCRM.Api.Extensions
{
    public static class RegisterServicesExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
