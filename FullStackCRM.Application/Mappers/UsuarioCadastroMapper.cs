using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Shared;

namespace FullStackCRM.Application.Mappers
{
    public class UsuarioCadastroMapper : Profile
    {
        public UsuarioCadastroMapper()
        {
            CreateMap<Usuario, UsuarioCadastroModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.PerfilId, p => p.MapFrom(x => x.Perfil.GetEnumValue()))
                .ForMember(p => p.Senha, p => p.MapFrom(x => x.Senha));

            CreateMap<UsuarioCadastroModel, Usuario>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Perfil, p => p.MapFrom(x => x.PerfilId.Value.GetEnum<EPerfis>()))
                .ForMember(p => p.Senha, p => p.MapFrom(x => x.Senha));
        }
    }
}
