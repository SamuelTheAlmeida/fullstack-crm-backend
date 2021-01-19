using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using FullStackCRM.Shared;
using System.Linq;

namespace FullStackCRM.Tests.Application.Services.UsuarioServiceTests.cs
{
    public class AutenticarUsuarioTests : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;
        public AutenticarUsuarioTests(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task AutenticarComSucesso(LoginModel loginModel)
        {
            // Arrange
            var usuario = fixture.Mapper.Map<Usuario>(loginModel);
            usuario.Perfil = EPerfis.Administrador;
            usuario.Id = Guid.NewGuid();

            fixture.UsuarioRepository.Setup(x => x
            .AutenticarAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => usuario);

            // Act
            var result = await fixture.UsuarioService.Autenticar(loginModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().NotBeNull();
            result.Dados.Id.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(DadosInvalidos))]
        public async Task AutenticarComDadosInvalidos(LoginModel loginModel)
        {
            // Arrange
            var usuario = fixture.Mapper.Map<Usuario>(loginModel);

            fixture.UsuarioRepository.Setup(x => x
            .AutenticarAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => usuario);


            // Act
            var result = await fixture.UsuarioService.Autenticar(loginModel);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.Dados.Should().BeNull();
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task AutenticarComSenhaIncorreta_RetornaErro(LoginModel loginModel)
        {
            // Arrange
            var usuario = fixture.Mapper.Map<Usuario>(loginModel);

            fixture.UsuarioRepository.Setup(x => x
            .AutenticarAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => default);


            // Act
            var result = await fixture.UsuarioService.Autenticar(loginModel);

            // Assert
            result.Sucesso.Should().BeFalse();
            result.Dados.Should().BeNull();
            result.Mensagens.Select(x => x.Descricao)
                .Should()
                .Contain(
                    EMensagens.EmailOuSenhaInvalidos.GetEnumDescription()
                );
        }

        public static IEnumerable<object[]> DadosValidos
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new LoginModel()
                        {
                            Email = "samuel.t.almeida@gmail.com",
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
                        new LoginModel()
                        {
                            Email = "samuel.t.almeida@gmail.com",
                            Senha = "123456"
                        }
                    },
                    new object[]
                    {
                        new LoginModel()
                        {
                            Email = "samuel.t.almeida",
                            Senha = "123456"
                        }
                    }
                };
            }
        }
    }
}
