using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using Moq;
using System;
using System.Collections.Generic;

namespace FullStackCRM.Tests.Application.Services.Fixture
{
    public class UsuarioFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public IUsuarioService UsuarioService { get; set; }
        public Mock<IUsuarioRepository> UsuarioRepository { get; set; }

        public List<Usuario> DbUsuarios = new List<Usuario>()
        {
            new Usuario()
            {
                Id = Guid.Parse("5e6aeb2a-d738-4b9b-9425-f26c6d61f5a5"),
                Email = "samuel.t.almeida@gmail.com",
                Perfil = EPerfis.Administrador,
                Senha =  ("123456").GetMd5Hash()
            },
            new Usuario()
            {
                Id = Guid.Parse("73cc0b0e-aaec-4ae1-a95c-9c4deeba81f3"),
                Email = "jose@gmail.com",
                Perfil = EPerfis.Funcionario,
                Senha =  ("2z#$@!dA15@!#").GetMd5Hash()
            },
        };

        public List<EnumModel> DbPerfis = new List<EnumModel>()
        {

        };


        public Guid ID_EXISTENTE = Guid.Parse("5e6aeb2a-d738-4b9b-9425-f26c6d61f5a5");
        public Guid ID_NAO_EXISTENTE = Guid.NewGuid();

        public UsuarioFixture()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(
                new Type[] {
                    typeof(UsuarioModel),
                    typeof(LoginModel)}
                )
            );
            Mapper = config.CreateMapper();

            UsuarioRepository = new Mock<IUsuarioRepository>();
            var tokenService = new Mock<ITokenService>();
            UsuarioService = new UsuarioService(Mapper, UsuarioRepository.Object, tokenService.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UsuarioFixture()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbUsuarios = null;
            }
        }
    }
}
