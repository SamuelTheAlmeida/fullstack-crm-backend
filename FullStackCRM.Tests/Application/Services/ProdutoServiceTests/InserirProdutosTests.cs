using AutoMapper;
using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace FullStackCRM.Tests.Application.Services.ProdutoServiceTests
{
    public class InserirProdutosTests : IClassFixture<ProdutoFixture>
    {
        private readonly ProdutoFixture fixture;
        public InserirProdutosTests(ProdutoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task InserirComDadosValidos(ProdutoModel produtoModel)
        {
            // Arrange
            var produto = fixture.Mapper.Map<Produto>(produtoModel);
            produto.Id = Guid.NewGuid();

            fixture.ProdutoRepository.Setup(x => x
            .InserirAsync(It.IsAny<Produto>()))
                .ReturnsAsync(() => produto);

            // Act
            var result = await fixture.ProdutoService.InserirAsync(produtoModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().NotBeNull();
            result.Dados.Id.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(DadosInvalidos))]
        public async Task InserirComDadosInvalidos(ProdutoModel produtoModel)
        {
            // Arrange
            fixture.ProdutoRepository.Setup(x => x
            .InserirAsync(It.IsAny<Produto>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await fixture.ProdutoService.InserirAsync(produtoModel);

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
                    new object[] { new ProdutoModel() { Nome = "produto 1", Preco = "9" } },
                    new object[] { new ProdutoModel() { Nome = "produto 2", Preco = "99.99" } },
                    new object[] { new ProdutoModel() { Nome = "produto 3", Preco = "999999.99" } }
                };
            }
        }

        public static IEnumerable<object[]> DadosInvalidos
        {
            get
            {
                return new[]
                {
                    new object[] { new ProdutoModel() { Nome = "", Preco = "9" } },
                    new object[] { new ProdutoModel() { Nome = null, Preco = "9" } },
                    new object[] { new ProdutoModel() { Nome = "produto 2", Preco = "" } },
                    new object[] { new ProdutoModel() { Nome = "produto 2", Preco = null } },
                };
            }
        }

    }
}
