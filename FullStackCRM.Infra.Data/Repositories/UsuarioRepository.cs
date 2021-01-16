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

    }
}
