using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;

namespace FullStackCRM.Application.Mappers
{
    public class PedidoMapper : Profile
    {
        public PedidoMapper()
        {
            CreateMap<Pedido, PedidoModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.EmailComprador, p => p.MapFrom(x => x.EmailComprador))
                .ForMember(p => p.Valor, p => p.MapFrom(x => x.Valor))
                .ReverseMap();
        }
    }
}
