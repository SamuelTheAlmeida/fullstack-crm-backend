﻿using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Application.Validators;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<BaseModel<List<ProdutoModel>>> ListarAsync(string nome)
        {
            var produtos = await _produtoRepository
                .ListarAsync(nome);

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
            var validator = await new ProdutoModelValidator().ValidateAsync(produtoModel);
            if (!validator.IsValid)
            {
                return new BaseModel<ProdutoModel>(false, validator.Errors);
            }

            var produto = _mapper.Map<Produto>(produtoModel);
            var result = _mapper.Map<ProdutoModel>(await _produtoRepository.AtualizarAsync(produto));
            return new BaseModel<ProdutoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<ProdutoModel>> InserirAsync(ProdutoModel produtoModel)
        {
            var validator = await new ProdutoModelValidator().ValidateAsync(produtoModel);
            if (!validator.IsValid)
            {
                return new BaseModel<ProdutoModel>(false, validator.Errors);
            }

            var produto = _mapper.Map<Produto>(produtoModel);
            var result = _mapper.Map<ProdutoModel>(await _produtoRepository.InserirAsync(produto));
            return new BaseModel<ProdutoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<ProdutoModel>> ExcluirAsync(Guid id)
        {
            await _produtoRepository.ExcluirAsync(id);
            return new BaseModel<ProdutoModel>(true, EMensagens.RealizadaComSucesso);
        }
    }
}
