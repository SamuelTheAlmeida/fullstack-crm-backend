using FullStackCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Domain.Repositories
{
    public interface IPedidoRepository
    {
        Task<Pedido> InserirAsync(Pedido pedido);
        Task<Pedido> AtualizarAsync(Pedido pedido);
        Task<List<Pedido>> ListarAsync();
        Task<Pedido> ObterPorIdAsync(Guid id);
        Task ExcluirAsync(Guid id);
    }
}
