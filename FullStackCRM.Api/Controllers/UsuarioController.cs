using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var response = await _usuarioService.ListarAsync();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _usuarioService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] UsuarioModel usuarioModel)
        {
            if (usuarioModel is null)
            {
                return BadRequest();
            }

            var response = await _usuarioService.InserirAsync(usuarioModel);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] UsuarioModel usuarioModel)
        {
            if (usuarioModel is null)
            {
                return BadRequest();
            }

            var response = await _usuarioService.AtualizarAsync(usuarioModel);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _usuarioService.ExcluirAsync(id);
            return Ok(response);
        }
    }
}