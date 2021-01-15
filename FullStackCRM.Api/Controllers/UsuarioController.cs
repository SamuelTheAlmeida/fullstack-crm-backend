using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FullStackCRM.Api.Controllers {

    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public UsuarioController(IUsuarioService usuarioService,
            ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioModel model)
        {
            var usuario = await _usuarioService.Autenticar(model.Email, model.Senha);
            if (usuario == default)
            {
                return NotFound(new { message = "Email ou senha inválidos." });
            }

            var token = _tokenService.GenerateToken(usuario);

            return Ok(new
            {
                token,
                usuario
            });
        } 
    }
}