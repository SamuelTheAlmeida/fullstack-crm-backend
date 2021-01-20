using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FullStackCRM.Api.Controllers
{
    [Produces("application/json")]
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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _usuarioService.Autenticar(model);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Get()
        {
            var response = await _usuarioService.ListarAsync();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _usuarioService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioCadastroModel usuarioCadastroModel)
        {
            if (usuarioCadastroModel is null)
            {
                return BadRequest();
            }

            var response = await _usuarioService.InserirAsync(usuarioCadastroModel);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Put([FromBody] UsuarioCadastroModel usuarioCadastroModel)
        {
            if (usuarioCadastroModel is null)
            {
                return BadRequest();
            }

            var response = await _usuarioService.AtualizarAsync(usuarioCadastroModel);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _usuarioService.ExcluirAsync(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("perfis")]
        public IActionResult GetPerfis()
        {
            var response = _usuarioService.ListarPerfis();
            return Ok(response);
        }
    }
}