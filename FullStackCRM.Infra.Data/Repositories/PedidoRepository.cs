using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackCRM.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string connectionString;
        public PedidoRepository()
        {
            connectionString = ConfigurationHelper.ConnectionString;
        }

        public async Task<List<Pedido>> ListarAsync()
        {
            var pedidos = new List<Pedido>();
            var sql =
            " SELECT                        " +
            "   Id,                         " +
            "   EmailComprador,             " +
            "   Valor                       " +
            " FROM                          " +
            "   Pedido                      " +
            " WHERE                         " +
            "   1=1                         ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    pedidos.Add(new Pedido()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal(nameof(Pedido.Id))),
                        EmailComprador = reader.GetString(reader.GetOrdinal(nameof(Pedido.EmailComprador))),
                        Valor = reader.GetDecimal(reader.GetOrdinal(nameof(Pedido.Valor)))
                    });
                }
            }
            return pedidos;
        }

        public async Task<Pedido> ObterPorIdAsync(Guid id)
        {
            var sql =
            " SELECT                                     " +
            "   PE.Id,                                   " +
            "   PE.EmailComprador,                       " +
            "   PE.Valor,                                " +
            "   PP.Quantidade,                           " +
            "   PP.PrecoUnitario,                        " +
            "   PP.PrecoTotal,                           " +
            "   PR.Id AS ProdutoId,                      " +
            "   PR.Nome                                  " +
            " FROM                                       " +
            "   Pedido PE                                " +
            " INNER JOIN                                 " +
            "   ProdutoPedido PP                         " +
            "   ON PP.PedidoId = PE.Id                   " +
            " INNER JOIN                                 " +
            "   Produto PR                               " +
            "   ON PR.Id = PP.ProdutoId                  " +
            " WHERE                                      " +
            "   PE.Id = @id                              ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", id.ToString().ToUpper());
                using var reader = await command.ExecuteReaderAsync();

                var pedido = new Pedido
                {
                    ProdutosPedido = new List<ProdutoPedido>()
                };
                while (await reader.ReadAsync())
                {
                    pedido.Id = reader.GetGuid(reader.GetOrdinal(nameof(Pedido.Id)));
                    pedido.EmailComprador = reader.GetString(reader.GetOrdinal(nameof(Pedido.EmailComprador)));
                    pedido.Valor = reader.GetDecimal(reader.GetOrdinal(nameof(Pedido.Valor)));
                    pedido.ProdutosPedido.Add(new ProdutoPedido()
                    {
                        PedidoId = reader.GetGuid(reader.GetOrdinal(nameof(Pedido.Id))),
                        PrecoUnitario = reader.GetDecimal(reader.GetOrdinal(nameof(ProdutoPedido.PrecoUnitario))),
                        Quantidade = reader.GetInt32(reader.GetOrdinal(nameof(ProdutoPedido.Quantidade))),
                        PrecoTotal = reader.GetDecimal(reader.GetOrdinal(nameof(ProdutoPedido.PrecoTotal))),
                        ProdutoId = reader.GetGuid(reader.GetOrdinal("ProdutoId")),
                        Nome = reader.GetString(reader.GetOrdinal(nameof(Produto.Nome)))
                    });
                }
                connection.Close();
                return pedido;
            }
        }

        public async Task<Pedido> InserirAsync(Pedido pedido)
        {
            var sql =
            " INSERT INTO Pedido (                   " +
            "   Id,                                  " +
            "   EmailComprador,                      " +
            "   Valor                                " +
            " )                                      " +
            "   VALUES                               " +
            " (@id, @emailComprador, @valor);        ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = sql;
                    var guid = Guid.NewGuid();
                    pedido.Id = guid;
                    insertCommand.Parameters.AddWithValue("@id", guid.ToString().ToUpper());
                    insertCommand.Parameters.AddWithValue("@emailComprador", pedido.EmailComprador);
                    insertCommand.Parameters.AddWithValue("@valor", pedido.Valor);
                    insertCommand.Transaction = transaction;
                    await insertCommand.ExecuteNonQueryAsync();

                    pedido.ProdutosPedido.ForEach(item => 
                        InserirProdutoPedido(item, pedido, connection, transaction));

                    transaction.Commit();
                    connection.Close();
                    return pedido;
                }
            }
        }

        public async Task<Pedido> AtualizarAsync(Pedido pedido)
        {
            var itensPedidoAntigo = (await ObterPorIdAsync(pedido.Id.Value)).ProdutosPedido.Select(x => x.ProdutoId);
            var itensPedidoNovo = pedido.ProdutosPedido.Select(x => x.ProdutoId);

            var produtosRemover = itensPedidoAntigo
                .Where(x => itensPedidoNovo
                    .All(y => y != x));

            var produtosAdicionar = itensPedidoNovo
                .Where(x => itensPedidoAntigo
                    .All(y => y != x));

            var produtosAtualizar = itensPedidoAntigo
                .Where(x => itensPedidoNovo
                    .Any(y => y == x));

            var sql =
            " UPDATE Pedido                             " +
            " SET                                       " +
            "   EmailComprador = @emailComprador,       " +
            "   Valor = @valor                          " +
            " WHERE                                     " +
            "   Id = @id;                               ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@id", pedido.Id.ToString().ToUpper());
                    command.Parameters.AddWithValue("@emailComprador", pedido.EmailComprador);
                    command.Parameters.AddWithValue("@valor", pedido.Valor);
                    command.Transaction = transaction;
                    await command.ExecuteNonQueryAsync();

                    pedido.ProdutosPedido
                        .Where(x => produtosAtualizar.Contains(x.ProdutoId))
                        .ToList()
                        .ForEach(item =>
                            AtualizarProdutoPedido(item, pedido, connection, transaction));

                    pedido.ProdutosPedido
                        .Where(x => produtosAdicionar.Contains(x.ProdutoId))
                        .ToList()
                        .ForEach(item =>
                            InserirProdutoPedido(item, pedido, connection, transaction));

                    produtosRemover
                        .ToList()
                        .ForEach(item =>
                            ExcluirProdutoPedido(item.Value, pedido.Id.Value, connection, transaction));

                    transaction.Commit();
                    connection.Close();
                    return pedido;
                }
            }
        }

        public async Task ExcluirAsync(Guid id)
        {
            var sql =
            " DELETE FROM                       " +
            "   ProdutoPedido                   " +
            " WHERE PedidoId = @id;             " +
            " DELETE FROM                       " +
            "   Pedido                          " +
            " WHERE Id = @id;                   ";


            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", id.ToString().ToUpper());
                await command.ExecuteNonQueryAsync();

                connection.Close();
            }
        }



        private void InserirProdutoPedido(ProdutoPedido item, Pedido pedido, SqlConnection connection, SqlTransaction transaction)
        {
            var sql =
            " INSERT INTO ProdutoPedido (                                           " +
            "   ProdutoId,                                                          " +
            "   PedidoId,                                                           " +
            "   Quantidade,                                                         " +
            "   PrecoUnitario,                                                      " +
            "   PrecoTotal                                                          " +
            " )                                                                     " +
            "   VALUES                                                              " +
            " (@produtoId, @pedidoId, @quantidade, @precoUnitario, @precoTotal);    ";
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@produtoId", item.ProdutoId);
            command.Parameters.AddWithValue("@pedidoId", pedido.Id);
            command.Parameters.AddWithValue("@quantidade", item.Quantidade);
            command.Parameters.AddWithValue("@precoUnitario", item.PrecoUnitario);
            command.Parameters.AddWithValue("@precoTotal", item.PrecoTotal);
            command.Transaction = transaction;
            command.ExecuteNonQuery();
        }

        private void AtualizarProdutoPedido(ProdutoPedido item, Pedido pedido, SqlConnection connection, SqlTransaction transaction)
        {
            var sql =
            " UPDATE ProdutoPedido                                  " +
            " SET                                                   " +
            "   Quantidade = @quantidade,                           " +
            "   PrecoUnitario = @precoUnitario,                     " +
            "   PrecoTotal = @precoTotal                            " +
            " WHERE                                                 " +
            "   ProdutoId = @produtoId AND PedidoId = @pedidoId     ";
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@produtoId", item.ProdutoId);
            command.Parameters.AddWithValue("@pedidoId", pedido.Id);
            command.Parameters.AddWithValue("@quantidade", item.Quantidade);
            command.Parameters.AddWithValue("@precoUnitario", item.PrecoUnitario);
            command.Parameters.AddWithValue("@precoTotal", item.PrecoTotal);
            command.Transaction = transaction;
            command.ExecuteNonQuery();
        }

        private void ExcluirProdutoPedido(Guid idProduto, Guid idPedido, SqlConnection connection, SqlTransaction transaction)
        {
            var sql =
            " DELETE FROM                   " +
            "   ProdutoPedido               " +
            " WHERE 1=1                     " +
            "   AND ProdutoId = @produtoId  " +
            "   AND PedidoId = @pedidoId    ";
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("@produtoId", idProduto);
            command.Parameters.AddWithValue("@pedidoId", idPedido);
            command.Transaction = transaction;
            command.ExecuteNonQuery();
        }
    }
}
