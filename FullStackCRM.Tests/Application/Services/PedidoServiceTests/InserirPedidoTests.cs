using FluentAssertions;
using FullStackCRM.Application.Models;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Tests.Application.Services.Fixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.PedidoServiceTests
{
    public class InserirPedidoTests : IClassFixture<PedidoFixture>
    {
        private readonly PedidoFixture fixture;
        public InserirPedidoTests(PedidoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task InserirComDadosValidos(PedidoModel pedidoModel)
        {
            // Arrange
            var pedido = fixture.Mapper.Map<Pedido>(pedidoModel);
            pedido.Id = Guid.NewGuid();

            fixture.PedidoRepository.Setup(x => x
            .InserirAsync(It.IsAny<Pedido>()))
                .ReturnsAsync(() => pedido);

            // Act
            var result = await fixture.PedidoService.InserirAsync(pedidoModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().NotBeNull();
            result.Dados.Id.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(DadosInvalidos))]
        public async Task InserirComDadosInvalidos(PedidoModel pedidoModel)
        {
            // Arrange
            fixture.PedidoRepository.Setup(x => x
            .InserirAsync(It.IsAny<Pedido>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await fixture.PedidoService.InserirAsync(pedidoModel);

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
                    new object[]
                    {
                        new PedidoModel()
                        {
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 300M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    ProdutoId = Guid.Parse("0af9adbe-4662-419f-b7bb-3f43089320df"),
                                    Nome = "Produto teste 01",
                                    PrecoUnitario = 100M,
                                    Quantidade = 3,
                                    PrecoTotal = 3*100M
                                }
                            }
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> DadosInvalidos
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new PedidoModel() // Pedido com valor total incorreto
                        {
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 1M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    ProdutoId = Guid.Parse("0af9adbe-4662-419f-b7bb-3f43089320df"),
                                    Nome = "Produto teste 01",
                                    PrecoUnitario = 100M,
                                    Quantidade = 3,
                                    PrecoTotal = 3*100M
                                }
                            }
                        }
                    },
                    new object[]
                    {
                        new PedidoModel() // Pedido com valor total do produto incorreto
                        {
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 300M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    ProdutoId = Guid.Parse("0af9adbe-4662-419f-b7bb-3f43089320df"),
                                    Nome = "Produto teste 01",
                                    PrecoUnitario = 100M,
                                    Quantidade = 3,
                                    PrecoTotal = 3M
                                }
                            }
                        }
                    },
                    new object[]
                    {
                        // Campos sem preenchimento
                        new PedidoModel()
                        {
                            EmailComprador = "",
                            Valor = 300M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    ProdutoId = Guid.Parse("0af9adbe-4662-419f-b7bb-3f43089320df"),
                                    Nome = "Produto teste 01",
                                    PrecoUnitario = 100M,
                                    Quantidade = 3,
                                    PrecoTotal = 3M
                                }
                            }
                        }
                    },
                    new object[]
                    {
                        new PedidoModel()
                        {
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 0,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    ProdutoId = Guid.Parse("0af9adbe-4662-419f-b7bb-3f43089320df"),
                                    Nome = "Produto teste 01",
                                    PrecoUnitario = 100M,
                                    Quantidade = 3,
                                    PrecoTotal = 3M
                                }
                            }
                        }
                    },
                    new object[]
                    {
                        new PedidoModel()
                        {
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 100M,
                            ProdutosPedido = new List<ProdutoPedidoModel>() { }
                        }
                    }
                };
            }
        }
    }
}
