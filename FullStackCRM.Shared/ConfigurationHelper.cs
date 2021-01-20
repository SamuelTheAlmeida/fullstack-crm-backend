using Microsoft.Extensions.Configuration;

namespace FullStackCRM.Shared
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration;

        public static void CarregarConfiguracoes(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Configurações de aplicação
        public static string ConnectionString => _configuration.GetConnectionString("DefaultConnection");
        public static string PrivateKey => _configuration["PrivateKey"];
        #endregion

        #region Configurações RabbitMq

        public static IConfigurationSection RabbitMqConfiguration => _configuration.GetSection("RabbitMq");
        public static IConfigurationSection RabbitMqExchangeConfiguration => _configuration.GetSection("RabbitMqExchange");
        public static string RabbitMqExchange => _configuration["RabbitMqCustomConfig:Exchange"];
        public static string RabbitMqRoutingKey => _configuration["RabbitMqCustomConfig:RoutingKey"];
        #endregion

        #region Configurações KissLog
        public static KissLogConfiguration KissLogConfiguration => new KissLogConfiguration()
        {
            OrganizationId = _configuration["KissLog.OrganizationId"],
            ApplicationId = _configuration["KissLog.ApplicationId"],
            ApiUrl = _configuration["KissLog.ApiUrl"]
        };
        #endregion
    }

    public class KissLogConfiguration
    {
        public string OrganizationId { get; set; }
        public string ApplicationId { get; set; }
        public string ApiUrl { get; set; }
    }


}
