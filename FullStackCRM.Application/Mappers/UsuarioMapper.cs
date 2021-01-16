using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Mappers
{
    public class UsuarioMapper : Profile
    {
        public UsuarioMapper()
        {
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Perfil, p => p.MapFrom(x => Convert.ToInt32(x.Perfil)))
                .ForMember(p => p.Senha, p => p.MapFrom(x => x.Senha));

            CreateMap<UsuarioModel, Usuario>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Perfil, p => p.MapFrom(x => (EPerfis)x.Perfil))
                .ForMember(p => p.Senha, p => p.MapFrom(x => x.Senha));
        }
    }
}
