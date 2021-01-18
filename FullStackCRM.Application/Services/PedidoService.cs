﻿using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Application.Validators;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoService(IMapper mapper, IPedidoRepository pedidoRepository)
        {
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
        }
        public async Task<BaseModel<PedidoModel>> InserirAsync(PedidoModel pedidoModel)
        {
            var validator = await new PedidoModelValidator().ValidateAsync(pedidoModel);
            if (!validator.IsValid)
            {
                return new BaseModel<PedidoModel>(false, validator.Errors);
            }

            var pedido = _mapper.Map<Pedido>(pedidoModel);
            var result = _mapper.Map<PedidoModel>(await _pedidoRepository.InserirAsync(pedido));
            return new BaseModel<PedidoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<PedidoModel>> AtualizarAsync(PedidoModel pedidoModel)
        {
            var validator = await new PedidoModelValidator().ValidateAsync(pedidoModel);
            if (!validator.IsValid)
            {
                return new BaseModel<PedidoModel>(false, validator.Errors);
            }

            var pedido = _mapper.Map<Pedido>(pedidoModel);
            var result = _mapper.Map<PedidoModel>(await _pedidoRepository.AtualizarAsync(pedido));
            return new BaseModel<PedidoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<List<PedidoModel>>> ListarAsync()
        {
            var result = _mapper.Map<List<PedidoModel>>(await _pedidoRepository.ListarAsync());
            return new BaseModel<List<PedidoModel>>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<PedidoModel>> ObterPorIdAsync(Guid id)
        {
            var pedido = await _pedidoRepository
                .ObterPorIdAsync(id);

            var result = _mapper.Map<PedidoModel>(pedido);
            return new BaseModel<PedidoModel>(true, EMensagens.RealizadaComSucesso, result);
        }

        public async Task<BaseModel<PedidoModel>> ExcluirAsync(Guid id)
        {
            await _pedidoRepository.ExcluirAsync(id);
            return new BaseModel<PedidoModel>(true, EMensagens.RealizadaComSucesso);
        }
    }
}
