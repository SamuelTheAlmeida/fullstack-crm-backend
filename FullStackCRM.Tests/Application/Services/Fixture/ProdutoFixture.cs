using AutoMapper;
using FullStackCRM.Application.Models;
using FullStackCRM.Application.Services;
using FullStackCRM.Application.Services.Interfaces;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;

namespace FullStackCRM.Tests.Application.Services.Fixture
{
    public class ProdutoFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public IProdutoService ProdutoService { get; set; }
        public Mock<IProdutoRepository> ProdutoRepository { get; set; }

        public List<Produto> DbProdutos = new List<Produto>()
        {
            new Produto()
            {
                Id = Guid.Parse("6dfc5cc2-47d6-4680-9f79-aabd78472afb"),
                Nome = "Produto teste 01",
                Preco = 19.90M
            },
            new Produto()
            {
                Id = Guid.Parse("0ce84d53-1cbf-432d-8df0-ca64a618078a"),
                Nome = "Produto teste 02",
                Preco = 1500.00M
            }
        };

        public string NOME_PRODUTO_EXISTENTE = "Produto teste 01";
        public string NOME_PRODUTO_NAO_EXISTENTE = "Produto imaginário";
        public Guid ID_EXISTENTE = Guid.Parse("0ce84d53-1cbf-432d-8df0-ca64a618078a");
        public Guid ID_NAO_EXISTENTE = Guid.NewGuid();


        public ProdutoFixture()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(ProdutoModel)));
            Mapper = config.CreateMapper();

            ProdutoRepository = new Mock<IProdutoRepository>();
            ProdutoService = new ProdutoService(Mapper, ProdutoRepository.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ProdutoFixture()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbProdutos = null;
            }
        }
    }
}
