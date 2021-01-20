using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.ProdutoServiceTests
{
    public class ObterProdutoPorIdTests : IClassFixture<ProdutoFixture>
    {
        private readonly ProdutoFixture fixture;

        public ObterProdutoPorIdTests(ProdutoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ObterPorIdExistente()
        {
            // Arrange
            var id = fixture.ID_EXISTENTE;
            var produtos = fixture.DbProdutos;

            fixture.ProdutoRepository.Setup(x => x
            .ObterPorIdAsync(id))
                .ReturnsAsync(() => produtos.FirstOrDefault(x => x.Id == id));

            var map = fixture.Mapper.Map<ProdutoModel>(produtos.FirstOrDefault(x => x.Id == id));

            // Act
            var result = await fixture.ProdutoService.ObterPorIdAsync(id);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ObterPorIdNaoExistente()
        {
            // Arrange
            var id = fixture.ID_NAO_EXISTENTE;
            var produtos = fixture.DbProdutos;

            fixture.ProdutoRepository.Setup(x => x
            .ObterPorIdAsync(id))
                .ReturnsAsync(() => produtos.FirstOrDefault(x => x.Id == id));

            // Act
            var result = await fixture.ProdutoService.ObterPorIdAsync(id);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeNull();
        }
    }
}
