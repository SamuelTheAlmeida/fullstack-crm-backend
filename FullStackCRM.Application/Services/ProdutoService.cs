using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FullStackCRM.Domain.Enums;

namespace FullStackCRM.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IMapper mapper, IProdutoRepository produtoRepository)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }
        public async Task<BaseModel<List<ProdutoModel>>> ListarAsync()
        {
            var produtos = await _produtoRepository
                .ListarAsync();

            var result = _mapper.Map<List<ProdutoModel>>(produtos);
            return new BaseModel<List<ProdutoModel>>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<ProdutoModel>> ObterPorIdAsync(Guid id)
        {
            var produto = await _produtoRepository
                .ObterPorIdAsync(id);

            var result = _mapper.Map<ProdutoModel>(produto);
            return new BaseModel<ProdutoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<ProdutoModel>> AtualizarAsync(ProdutoModel produtoModel)
        {
            var produto = _mapper.Map<Produto>(produtoModel);
            var result = _mapper.Map<ProdutoModel>(await _produtoRepository.AtualizarAsync(produto));
            return new BaseModel<ProdutoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<ProdutoModel>> InserirAsync(ProdutoModel produtoModel)
        {
            var produto = _mapper.Map<Produto>(produtoModel);
            var result = _mapper.Map<ProdutoModel>(await _produtoRepository.InserirAsync(produto));
            return new BaseModel<ProdutoModel>(true, EMensagens.RealizadaComSucesso, result);
        }
    }
}
