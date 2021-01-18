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
        public static KissLogConfiguration KissLogConfiguration => new KissLogConfiguration()
        {
            OrganizationId = _configuration["KissLog.OrganizationId"],
            ApplicationId = _configuration["KissLog.ApplicationId"],
            ApiUrl = _configuration["KissLog.ApiUrl"]
        };
    }

    public class KissLogConfiguration
    {
        public string OrganizationId { get; set; }
        public string ApplicationId { get; set; }
        public string ApiUrl { get; set; }
    }
}
