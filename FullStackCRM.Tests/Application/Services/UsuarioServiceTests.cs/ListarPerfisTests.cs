using FluentAssertions;
using FullStackCRM.Tests.Application.Services.Fixture;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.UsuarioServiceTests.cs
{
    public class ListarPerfisTests : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;
        public ListarPerfisTests(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ListarTodos()
        {
            // Arrange

            // Act
            var result = fixture.UsuarioService.ListarPerfis();

            // Assert
            result.Sucesso.Should().Be(true);
        }
    }
}
