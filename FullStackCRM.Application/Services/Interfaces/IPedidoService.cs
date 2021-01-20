using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<BaseModel<PedidoModel>> InserirAsync(PedidoModel pedidoModel);
        Task<BaseModel<PedidoModel>> AtualizarAsync(PedidoModel pedidoModel);
        Task<BaseModel<List<PedidoModel>>> ListarAsync();
        Task<BaseModel<PedidoModel>> ObterPorIdAsync(Guid id);
        Task<BaseModel<PedidoModel>> ExcluirAsync(Guid id);
    }
}
