using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Mappers
{
    public class ProdutoPedidoMapper : Profile
    {
        public ProdutoPedidoMapper()
        {
            CreateMap<ProdutoPedido, ProdutoPedidoModel>()
                .ForMember(p => p.ProdutoId, p => p.MapFrom(x => x.ProdutoId))
                .ForMember(p => p.Nome, p => p.MapFrom(x => x.Nome))
                .ForMember(p => p.PedidoId, p => p.MapFrom(x => x.PedidoId))
                .ForMember(p => p.Quantidade, p => p.MapFrom(x => x.Quantidade))
                .ForMember(p => p.PrecoUnitario, p => p.MapFrom(x => x.PrecoUnitario))
                .ForMember(p => p.PrecoTotal, p => p.MapFrom(x => x.PrecoTotal))
                .ReverseMap();
        }
    }
}
