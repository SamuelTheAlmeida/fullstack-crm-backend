using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Mappers
{
    public class ProdutoMapper : Profile
    {
        public ProdutoMapper()
        {
            CreateMap<Produto, ProdutoModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Nome, p => p.MapFrom(x => x.Nome))
                .ForMember(p => p.Preco, p => p.MapFrom(x => x.Preco.ToString()))
                .ReverseMap();
        }
    }
}
