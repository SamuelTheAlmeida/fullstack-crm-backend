using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.ProdutoServiceTests
{
    public class AtualizarProdutosTests : IClassFixture<ProdutoFixture>
    {
        private readonly ProdutoFixture fixture;
        public AtualizarProdutosTests(ProdutoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task AtualizarComDadosValidos(ProdutoModel produtoModel)
        {
            // Arrange
            var produto = fixture.Mapper.Map<Produto>(produtoModel);

            fixture.ProdutoRepository.Setup(x => x
            .AtualizarAsync(It.IsAny<Produto>()))
                .ReturnsAsync(() => produto);

            // Act
            var result = await fixture.ProdutoService.AtualizarAsync(produtoModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(DadosInvalidos))]
        public async Task AtualizarComDadosInvalidos(ProdutoModel produtoModel)
        {
            // Arrange
            fixture.ProdutoRepository.Setup(x => x
            .AtualizarAsync(It.IsAny<Produto>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await fixture.ProdutoService.AtualizarAsync(produtoModel);

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
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = "produto 1", Preco = "9" } },
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = "produto 2", Preco = "99.99" } },
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = "produto 3", Preco = "999999.99" } }
                };
            }
        }

        public static IEnumerable<object[]> DadosInvalidos
        {
            get
            {
                return new[]
                {
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = "", Preco = "9" } },
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = null, Preco = "9" } },
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = "produto 2", Preco = "" } },
                    new object[] { new ProdutoModel() { Id = Guid.NewGuid(), Nome = "produto 2", Preco = null } },
                };
            }
        }
    }
}
