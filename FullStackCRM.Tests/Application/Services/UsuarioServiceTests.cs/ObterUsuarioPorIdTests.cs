using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.UsuarioServiceTests.cs
{
    public class ObterUsuarioPorIdTests : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;
        public ObterUsuarioPorIdTests(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ObterPorIdExistente()
        {
            // Arrange
            var id = fixture.ID_EXISTENTE;
            var usuarios = fixture.DbUsuarios;

            fixture.UsuarioRepository.Setup(x => x
            .ObterPorIdAsync(id))
                .ReturnsAsync(() => usuarios.FirstOrDefault(x => x.Id == id));

            var map = fixture.Mapper.Map<UsuarioModel>(usuarios.FirstOrDefault(x => x.Id == id));

            // Act
            var result = await fixture.UsuarioService.ObterPorIdAsync(id);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ObterPorIdNaoExistente()
        {
            // Arrange
            var id = fixture.ID_NAO_EXISTENTE;
            var usuarios = fixture.DbUsuarios;

            fixture.UsuarioRepository.Setup(x => x
            .ObterPorIdAsync(id))
                .ReturnsAsync(() => usuarios.FirstOrDefault(x => x.Id == id));

            var map = fixture.Mapper.Map<UsuarioModel>(usuarios.FirstOrDefault(x => x.Id == id));

            // Act
            var result = await fixture.UsuarioService.ObterPorIdAsync(id);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeNull();
        }
    }
}
