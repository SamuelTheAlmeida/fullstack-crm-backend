using FullStackCRM.Domain.Repositories;
using FullStackCRM.Shared;
using RabbitMQ.Client.Core.DependencyInjection.Services;
using System.Threading.Tasks;

namespace FullStackCRM.Infra.Data.Repositories
{
    public class RabbitMqRepository : IRabbitMqRepository
    {
        private readonly IQueueService _queueService;
        public RabbitMqRepository(IQueueService queueService)
        {
            _queueService = queueService;
        }
        public async Task EnviarMensagemFilaEmail(string mensagem)
        {
            await _queueService.SendJsonAsync(
                mensagem,
                exchangeName: ConfigurationHelper.RabbitMqExchange,
                routingKey: ConfigurationHelper.RabbitMqRoutingKey);
        }
    }
}
