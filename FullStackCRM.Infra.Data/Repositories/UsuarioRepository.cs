using FullStackCRM.Domain;
using FullStackCRM.Domain.Entities;
using FullStackCRM.Domain.Enums;
using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCRM.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string connectionString;
        public UsuarioRepository()
        {
            connectionString = ConfigurationHelper.ConnectionString;
        }

        public async Task<Usuario> AutenticarAsync(string email, string senha)
        {
            var query = 
            " SELECT                        " +
            "   Id,                         " +
            "   Email,                      " +
            "   Perfil                      " +
            " FROM                          " +
            "   Usuario                     " +
            " WHERE                         " +
            "   1=1                         " +
            "   AND email = @email          " +
            "   AND senha = @senha          ";

                using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = query;
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@senha", senha);
                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    return new Usuario()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        Perfil = (EPerfis)reader.GetInt32(reader.GetOrdinal("Perfil"))
                    };
                }
            }
            return default;
        }

        public async Task<List<Usuario>> ListarAsync()
        {
            var usuarios = new List<Usuario>();
            var sql =
            " SELECT                        " +
            "   Id,                         " +
            "   Email,                      " +
            "   Perfil                      " +
            " FROM                          " +
            "   Usuario                     " +
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
                    usuarios.Add(new Usuario()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal(nameof(Usuario.Id))),
                        Email = reader.GetString(reader.GetOrdinal(nameof(Usuario.Email))),
                        Perfil = (EPerfis)reader.GetInt32(reader.GetOrdinal(nameof(Usuario.Perfil)))
                    });
                }
            }
            return usuarios;
        }

        public async Task<Usuario> InserirAsync(Usuario usuario)
        {
            var sql =
            " INSERT INTO Usuario (        " +
            "   Id,                        " +
            "   Email,                     " +
            "   Perfil,                     " +
            "   Senha                      " +
            " )                            " +
            "   VALUES                     " +
            " (@id, @email, @perfil, @senha);        ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = sql;
                    var guid = Guid.NewGuid();
                    usuario.Id = guid;
                    insertCommand.Parameters.AddWithValue("@id", guid.ToString().ToUpper());
                    insertCommand.Parameters.AddWithValue("@email", usuario.Email);
                    insertCommand.Parameters.AddWithValue("@perfil", usuario.Perfil.GetEnumValue());
                    insertCommand.Parameters.AddWithValue("@senha", usuario.Senha);
                    insertCommand.Transaction = transaction;
                    await insertCommand.ExecuteNonQueryAsync();

                    transaction.Commit();
                    connection.Close();

                    usuario.Senha = string.Empty;
                    return usuario;
                }
            }
        }

        public async Task<Usuario> AtualizarAsync(Usuario usuario)
        {
            var sql =
            " UPDATE Usuario            " +
            " SET                       " +
            "   Email = @email,         " +
            "   Perfil = @perfl,        " +
            "   Senha = @senha          " +
            " WHERE                     " +
            "   Id = @id;               ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.AddWithValue("@id", usuario.Id.ToString().ToUpper());
                command.Parameters.AddWithValue("@email", usuario.Email);
                command.Parameters.AddWithValue("@perfil", usuario.Perfil.GetEnumValue());
                command.Parameters.AddWithValue("@senha", usuario.Senha);
                await command.ExecuteNonQueryAsync();

                connection.Close();

                usuario.Senha = string.Empty;
                return usuario;
            }
        }

        public async Task<Usuario> ObterPorIdAsync(Guid id)
        {
            var sql =
            " SELECT TOP(1)                 " +
            "   Id,                         " +
            "   Email,                       " +
            "   Perfil                       " +
            " FROM                          " +
            "   Usuario                     " +
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
                    var usuario = new Usuario()
                    {
                        Id = reader.GetGuid(reader.GetOrdinal(nameof(Usuario.Id))),
                        Email = reader.GetString(reader.GetOrdinal(nameof(Usuario.Email))),
                        Perfil = (EPerfis)reader.GetInt32(reader.GetOrdinal(nameof(Usuario.Perfil)))
                    };
                    connection.Close();

                    return usuario;
                }
            }
            return default;
        }

        public async Task ExcluirAsync(Guid id)
        {
            var sql =
            " DELETE FROM            " +
            "   Usuario              " +
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

    }
}
