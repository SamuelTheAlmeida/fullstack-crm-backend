using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.PedidoServiceTests
{
    public class ListarPedidosTests : IClassFixture<PedidoFixture>
    {
        private readonly PedidoFixture fixture;

        public ListarPedidosTests(PedidoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ListarTodos()
        {
            // Arrange
            fixture.PedidoRepository.Setup(x => x
            .ListarAsync())
                .ReturnsAsync(() => fixture.DbPedidos);

            var map = fixture.Mapper.Map<List<PedidoModel>>(fixture.DbPedidos);

            // Act
            var result = await fixture.PedidoService.ListarAsync();

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }
    }
}
