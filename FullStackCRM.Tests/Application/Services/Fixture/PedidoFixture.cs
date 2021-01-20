using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Repositories;
using Moq;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System;
using System.Collections.Generic;

namespace FullStackCRM.Tests.Application.Services.Fixture
{
    public class PedidoFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public IPedidoService PedidoService { get; set; }
        public Mock<IPedidoRepository> PedidoRepository { get; set; }

        public List<Pedido> DbPedidos = new List<Pedido>()
        {
            new Pedido()
            {
                EmailComprador = "samuel.t.almeida@gmail.com",
                Id = Guid.Parse("6dfc5cc2-47d6-4680-9f79-aabd78472afb"),
                Valor = 500.00M,
                ProdutosPedido = new List<ProdutoPedido>()
                {
                    new ProdutoPedido()
                    {
                        ProdutoId = Guid.Parse("29514925-1e3a-4488-9da0-2a919fb25588"),
                        Nome = "Produto teste 01",
                        PedidoId = Guid.Parse("6dfc5cc2-47d6-4680-9f79-aabd78472afb"),
                        PrecoUnitario = 100M,
                        Quantidade = 3,
                        PrecoTotal = 3*100M
                    }
                }
            }
        };

        public Guid ID_EXISTENTE = Guid.Parse("6dfc5cc2-47d6-4680-9f79-aabd78472afb");
        public Guid ID_NAO_EXISTENTE = Guid.NewGuid();

        public PedidoFixture()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(PedidoModel)));
            Mapper = config.CreateMapper();

            PedidoRepository = new Mock<IPedidoRepository>();
            var rabbitMqRepository = new Mock<IRabbitMqRepository>();
            PedidoService = new PedidoService(Mapper, PedidoRepository.Object, rabbitMqRepository.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~PedidoFixture()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbPedidos = null;
            }
        }
    }
}
