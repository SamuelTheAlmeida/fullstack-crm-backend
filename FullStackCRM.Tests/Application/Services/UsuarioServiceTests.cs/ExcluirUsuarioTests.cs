﻿using FluentAssertions;
using FullStackCRM.Tests.Application.Services.Fixture;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FullStackCRM.Tests.Application.Services.UsuarioServiceTests.cs
{
    public class ExcluirUsuarioTests : IClassFixture<UsuarioFixture>
    {
        private readonly UsuarioFixture fixture;

        public ExcluirUsuarioTests(UsuarioFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [InlineData(new object[] { "45448418-b806-4ff7-a084-a5bad9de9d47" })]
        public async Task Exclusao_DeveRetornarSucesso(string id)
        {
            // Arrange
            var idProduto = Guid.Parse(id);

            // Act
            var result = await fixture.UsuarioService.ExcluirAsync(idProduto);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().BeNull();
        }
    }
}
