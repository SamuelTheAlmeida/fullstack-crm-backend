using FullStackCRM.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<BaseModel<List<ProdutoModel>>> ListarAsync(string nome);
        Task<BaseModel<ProdutoModel>> InserirAsync(ProdutoModel produtoModel);
        Task<BaseModel<ProdutoModel>> AtualizarAsync(ProdutoModel produtoModel);
        Task<BaseModel<ProdutoModel>> ObterPorIdAsync(Guid id);
        Task<BaseModel<ProdutoModel>> ExcluirAsync(Guid id);
    }
}
