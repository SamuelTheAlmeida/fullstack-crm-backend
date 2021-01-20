using System.Threading.Tasks;

namespace FullStackCRM.Domain.Repositories
{
    public interface IRabbitMqRepository
    {
        Task EnviarMensagemFilaEmail(string mensagem);
    }
}
