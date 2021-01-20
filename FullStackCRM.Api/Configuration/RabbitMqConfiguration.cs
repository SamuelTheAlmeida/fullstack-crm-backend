using FullStackCRM.Shared;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;

namespace FullStackCRM.Api.Configuration
{
    public static class RabbitMqConfiguration
    {
        public static void AddRabbitMqConfiguration(this IServiceCollection services)
        {
            var rabbitMqSection = ConfigurationHelper.RabbitMqConfiguration;
            var exchangeSection = ConfigurationHelper.RabbitMqExchangeConfiguration;

            services.AddRabbitMqClient(rabbitMqSection)
                .AddProductionExchange(ConfigurationHelper.RabbitMqExchange, exchangeSection);
        }
    }
}
