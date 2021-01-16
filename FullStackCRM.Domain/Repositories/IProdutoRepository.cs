﻿using FullStackCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Domain.Repositories
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ListarAsync();
        Task<Produto> ObterPorIdAsync(Guid id);
        Task<Produto> InserirAsync(Produto produto);
        Task<Produto> AtualizarAsync(Produto produto);
    }
}
