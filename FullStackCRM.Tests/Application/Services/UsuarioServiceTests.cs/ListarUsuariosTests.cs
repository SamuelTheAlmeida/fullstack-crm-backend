using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.UsuarioServiceTests.cs
{
    public class ListarUsuariosTests : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;
        public ListarUsuariosTests(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ListarTodos()
        {
            // Arrange
            fixture.UsuarioRepository.Setup(x => x
            .ListarAsync())
                .ReturnsAsync(() => fixture.DbUsuarios);

            var map = fixture.Mapper.Map<List<UsuarioModel>>(fixture.DbUsuarios);

            // Act
            var result = await fixture.UsuarioService.ListarAsync();

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }
    }
}
