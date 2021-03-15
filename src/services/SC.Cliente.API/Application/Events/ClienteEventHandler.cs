using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace SC.Clientes.API.Application.Events
{
    public class ClienteEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        //trabalhar a manipulção dos eventos
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //pode ter class para enviar email
            // Enviar evento de confirmação
            return Task.CompletedTask;
        }
    }
}