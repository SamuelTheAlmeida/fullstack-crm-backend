using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Shared;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.UsuarioServiceTests.cs
{
    public class InserirUsuarioTests : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;
        public InserirUsuarioTests(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task InserirComDadosValidos(UsuarioCadastroModel usuarioCadastroModel)
        {
            // Arrange
            var usuario = fixture.Mapper.Map<Usuario>(usuarioCadastroModel);
            usuario.Id = Guid.NewGuid();

            fixture.UsuarioRepository.Setup(x => x
            .InserirAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(() => usuario);

            // Act
            var result = await fixture.UsuarioService.InserirAsync(usuarioCadastroModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().NotBeNull();
            result.Dados.Id.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(DadosInvalidos))]
        public async Task InserirComDadosInvalidos(UsuarioCadastroModel usuarioCadastroModel)
        {
            // Arrange
            fixture.UsuarioRepository.Setup(x => x
            .InserirAsync(It.IsAny<Usuario>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await fixture.UsuarioService.InserirAsync(usuarioCadastroModel);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.Dados.Should().BeNull();
        }

        public static IEnumerable<object[]> DadosValidos
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new UsuarioCadastroModel()
                        {
                            Email = "samuel.t.almeida@gmail.com",
                            PerfilId = (int)EPerfis.Administrador,
                            Senha = ("123456").GetMd5Hash()
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> DadosInvalidos
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        // Senha sem MD5
                        new UsuarioCadastroModel()
                        {
                            Email = "samuel.t.almeida@gmail.com",
                            PerfilId = (int)EPerfis.Administrador,
                            Senha = "123456"
                        }
                    },
                    // Campos não preenchidos
                    new object[]
                    {
                        new UsuarioCadastroModel()
                        {
                            Email = "",
                            PerfilId = (int)EPerfis.Administrador,
                            Senha = ("123456").GetMd5Hash()
                        }
                    },
                    new object[]
                    {

                        new UsuarioCadastroModel()
                        {
                            Email = "samuel.t.almeida",
                            PerfilId = 0,
                            Senha = ("123456").GetMd5Hash()
                        }
                    },
                    new object[]
                    {
                        new UsuarioCadastroModel()
                        {
                            Email = "samuel.t.almeida",
                            PerfilId = (int)EPerfis.Administrador,
                            Senha = null
                        }
                    },
                };
            }
        }

    }
}
