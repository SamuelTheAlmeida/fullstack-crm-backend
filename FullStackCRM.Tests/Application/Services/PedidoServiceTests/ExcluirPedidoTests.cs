using FluentAssertions;
using FullStackCRM.Tests.Application.Services.Fixture;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.PedidoServiceTests
{
    public class ExcluirPedidoTests : IClassFixture<PedidoFixture>
    {
        private readonly PedidoFixture fixture;
        public ExcluirPedidoTests(PedidoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(new object[] { "3c54e202-3488-4220-a6b3-5132d0fdf140" })]
        public async Task Exclusao_DeveRetornarSucesso(string id)
        {
            // Arrange
            var idPedido = Guid.Parse(id);

            // Act
            var result = await fixture.PedidoService.ExcluirAsync(idPedido);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().BeNull();
        }
    }
}
