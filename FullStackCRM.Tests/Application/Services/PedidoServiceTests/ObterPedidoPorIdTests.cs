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

namespace FullStackCRM.Tests.Application.Services.PedidoServiceTests
{
    public class ObterPedidoPorIdTests : IClassFixture<PedidoFixture>
    {
        private readonly PedidoFixture fixture;

        public ObterPedidoPorIdTests(PedidoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ObterPorIdExistente()
        {
            // Arrange
            var id = fixture.ID_EXISTENTE;
            var pedidos = fixture.DbPedidos;

            fixture.PedidoRepository.Setup(x => x
            .ObterPorIdAsync(id))
                .ReturnsAsync(() => pedidos.FirstOrDefault(x => x.Id == id));

            var map = fixture.Mapper.Map<PedidoModel>(pedidos.FirstOrDefault(x => x.Id == id));

            // Act
            var result = await fixture.PedidoService.ObterPorIdAsync(id);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ObterPorIdNaoExistente()
        {
            // Arrange
            var id = fixture.ID_NAO_EXISTENTE;
            var pedidos = fixture.DbPedidos;

            fixture.PedidoRepository.Setup(x => x
            .ObterPorIdAsync(id))
                .ReturnsAsync(() => pedidos.FirstOrDefault(x => x.Id == id));

            var map = fixture.Mapper.Map<PedidoModel>(pedidos.FirstOrDefault(x => x.Id == id));

            // Act
            var result = await fixture.PedidoService.ObterPorIdAsync(id);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeNull();
        }
    }
}
