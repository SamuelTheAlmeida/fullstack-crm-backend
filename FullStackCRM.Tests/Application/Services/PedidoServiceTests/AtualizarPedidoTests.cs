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
    public class AtualizarPedidoTests : IClassFixture<PedidoFixture>
    {
        private readonly PedidoFixture fixture;
        public AtualizarPedidoTests(PedidoFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(DadosValidos))]
        public async Task AtualizarComDadosValidos(PedidoModel pedidoModel)
        {
            // Arrange
            var pedido = fixture.Mapper.Map<Pedido>(pedidoModel);
            pedido.Id = Guid.NewGuid();

            fixture.PedidoRepository.Setup(x => x
            .AtualizarAsync(It.IsAny<Pedido>()))
                .ReturnsAsync(() => pedido);

            // Act
            var result = await fixture.PedidoService.AtualizarAsync(pedidoModel);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().NotBeNull();
            result.Dados.Id.Should().NotBeNull();
        }

        [Theory, MemberData(nameof(DadosInvalidos))]
        public async Task AtualizarComDadosInvalidos(PedidoModel pedidoModel)
        {
            // Arrange
            fixture.PedidoRepository.Setup(x => x
            .AtualizarAsync(It.IsAny<Pedido>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await fixture.PedidoService.AtualizarAsync(pedidoModel);

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
                            Id = Guid.Parse("c2f6859c-33f2-45eb-a6f1-70dd0ebc1e3e"),
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 300M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    PedidoId = Guid.Parse("c2f6859c-33f2-45eb-a6f1-70dd0ebc1e3e"),
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
                            Id = Guid.Parse("e63b734a-ec73-4791-9572-8a732d02a951"),
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 1M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    PedidoId = Guid.Parse("e63b734a-ec73-4791-9572-8a732d02a951"),
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
                            Id = Guid.Parse("5978f970-9583-44d7-af84-105920065f08"),
                            EmailComprador = "samuel.t.almeida@gmail.com",
                            Valor = 300M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    PedidoId = Guid.Parse("5978f970-9583-44d7-af84-105920065f08"),
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
                            Id = Guid.Parse("66f464fa-600f-4283-8d3a-6781ae75e73c"),
                            EmailComprador = "",
                            Valor = 300M,
                            ProdutosPedido = new List<ProdutoPedidoModel>()
                            {
                                new ProdutoPedidoModel()
                                {
                                    PedidoId = Guid.Parse("66f464fa-600f-4283-8d3a-6781ae75e73c"),
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
