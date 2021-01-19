using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackCRM.Application.Mappers
{
    public class LoginModelMapper : Profile
    {
        public LoginModelMapper()
        {
            CreateMap<Usuario, LoginModel>()
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Senha, p => p.MapFrom(x => x.Senha));

            CreateMap<LoginModel, Usuario>()
                .ForMember(p => p.Id, p => p.Ignore())
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Perfil, p => p.Ignore())
                .ForMember(p => p.Senha, p => p.MapFrom(x => x.Senha));
        }
    }
}
