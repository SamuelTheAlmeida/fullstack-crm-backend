using FullStackCRM.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullStackCRM.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Produtos";
        }

        [HttpPost]
        [AllowAnonymous]
        public void Login([FromBody] UsuarioModel model)
        {

        } 
    }
}