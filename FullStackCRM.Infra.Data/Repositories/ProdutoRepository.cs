using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly string connectionString;
        public ProdutoRepository()
        {
            connectionString = ConfigurationHelper.ConnectionString;
        }

        public async Task<List<Produto>> ListarAsync()
        {
            var produtos = new List<Produto>();
            var sql =
            " SELECT                        " +
            "   Id,                         " +
            "   Nome,                       " +
            "   Preco                       " +
            " FROM                          " +
            "   Produto                     " +
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
                    produtos.Add(new Produto()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal(nameof(Produto.Id))),
                        Nome = reader.GetString(reader.GetOrdinal(nameof(Produto.Nome))),
                        Preco = reader.GetDecimal(reader.GetOrdinal(nameof(Produto.Preco)))
                    });
                }
            }
            return produtos;
        }

        public async Task<Produto> InserirAsync(Produto produto)
        {
            var sql =
            " INSERT INTO Produto (        " +
            "   Id,                        " +
            "   Nome,                      " +
            "   Preco                      " +
            " )                            " +
            "   VALUES                     " +
            " (@id, @nome, @preco);        ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = sql;
                    var guid = Guid.NewGuid();
                    produto.Id = guid;
                    insertCommand.Parameters.AddWithValue("@id", guid.ToString().ToUpper());
                    insertCommand.Parameters.AddWithValue("@nome", produto.Nome);
                    insertCommand.Parameters.AddWithValue("@preco", produto.Preco);
                    insertCommand.Transaction = transaction;
                    await insertCommand.ExecuteNonQueryAsync();

                    transaction.Commit();
                    connection.Close();
                    return produto;
                }
            }
        }

        public async Task<Produto> AtualizarAsync(Produto produto)
        {
            var sql =
            " UPDATE Produto            " +
            " SET                       " +
            "   Nome = @nome,           " +
            "   Preco = @preco          " +
            " WHERE                     " +
            "   Id = @id;               ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", produto.Id.ToString().ToUpper());
                command.Parameters.AddWithValue("@nome", produto.Nome);
                command.Parameters.AddWithValue("@preco", produto.Preco);
                await command.ExecuteNonQueryAsync();

                connection.Close();
                return produto;
            }
        }

        public async Task ExcluirAsync(Guid id)
        {
            var sql =
            " DELETE FROM            " +
            "   Produto              " +
            " WHERE Id = @id;        ";

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

        public async Task<Produto> ObterPorIdAsync(Guid id)
        {
            var sql =
            " SELECT TOP(1)                 " +
            "   Id,                         " +
            "   Nome,                       " +
            "   Preco                       " +
            " FROM                          " +
            "   Produto                     " +
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
                    var produto = new Produto()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal(nameof(Produto.Id))),
                        Nome = reader.GetString(reader.GetOrdinal(nameof(Produto.Nome))),
                        Preco = reader.GetDecimal(reader.GetOrdinal(nameof(Produto.Preco)))
                    };
                    connection.Close();
                    return produto;
                }
            }
            return default;
        }
    }
}
