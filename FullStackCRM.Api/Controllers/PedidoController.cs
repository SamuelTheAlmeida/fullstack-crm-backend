using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FullStackCRM.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidoController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var response = await _pedidoService.ListarAsync();
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _pedidoService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PedidoModel pedidoModel)
        {
            if (pedidoModel is null)
            {
                return BadRequest();
            }

            var response = await _pedidoService.InserirAsync(pedidoModel);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] PedidoModel produtoModel)
        {
            if (produtoModel is null)
            {
                return BadRequest();
            }

            var response = await _pedidoService.AtualizarAsync(produtoModel);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _pedidoService.ExcluirAsync(id);
            return Ok(response);
        }
    }
}
