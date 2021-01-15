using Microsoft.Extensions.Configuration;
using System;

namespace FullStackCRM.Shared
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        public static void CarregarConfiguracoes(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

        public static string PrivateKey => _configuration["PrivateKey"];
    }
}
