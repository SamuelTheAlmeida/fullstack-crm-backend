using AutoMapper;
using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.ProdutoServiceTests
{
    public class ListarProdutosTests : IClassFixture<ProdutoFixture>
    {
        private readonly ProdutoFixture fixture;

        public ListarProdutosTests(ProdutoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ListarTodos()
        {
            // Arrange
            fixture.ProdutoRepository.Setup(x => x
            .ListarAsync(null))
                .ReturnsAsync(() => fixture.DbProdutos);

            var map = fixture.Mapper.Map<List<ProdutoModel>>(fixture.DbProdutos);

            // Act
            var result = await fixture.ProdutoService.ListarAsync(null);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ListarPorNome()
        {
            // Arrange
            var nome = fixture.NOME_PRODUTO_EXISTENTE;
            fixture.ProdutoRepository.Setup(x => x
            .ListarAsync(nome))
                .ReturnsAsync(() => fixture.DbProdutos.Where(x => x.Nome == nome).ToList());

            var map = fixture.Mapper.Map<List<ProdutoModel>>(fixture.DbProdutos.Where(x => x.Nome == nome).ToList());

            // Act
            var result = await fixture.ProdutoService.ListarAsync(nome);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ListarPorNomeNaoExistente()
        {
            // Arrange
            var nome = fixture.NOME_PRODUTO_NAO_EXISTENTE;
            fixture.ProdutoRepository.Setup(x => x
            .ListarAsync(nome))
                .ReturnsAsync(() => fixture.DbProdutos.Where(x => x.Nome == nome).ToList());

            // Act
            var result = await fixture.ProdutoService.ListarAsync(nome);

            // Assert
            result.Sucesso.Should().Be(true);
            result.Dados.Should().BeEmpty();
        }
    }
}
