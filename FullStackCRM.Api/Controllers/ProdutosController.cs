using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FullStackCRM.Api.Controllers {
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(string nome)
        {
            var response = await _produtoService.ListarAsync(nome);
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _produtoService.ObterPorIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] ProdutoModel produtoModel)
        {
            if (produtoModel is null)
            {
                return BadRequest();
            }

            var response = await _produtoService.InserirAsync(produtoModel);
            return Ok(response);
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Put([FromBody] ProdutoModel produtoModel)
        {
            if (produtoModel is null)
            {
                return BadRequest();
            }

            var response = await _produtoService.AtualizarAsync(produtoModel);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _produtoService.ExcluirAsync(id);
            return Ok(response);
        }
    }
}