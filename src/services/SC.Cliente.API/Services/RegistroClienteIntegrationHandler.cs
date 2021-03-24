using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SC.Clientes.API.Application.Commands;
using SC.Core.Mediator;
using SC.Core.Messages.Integration;
using SC.MessageBus;

namespace SC.Clientes.API.Services
{
    //manipular integração 
    //BackgroundService CRIANDO UM WORKER, EMBAIXO DO DOTNET
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;//é o startup

        public RegistroClienteIntegrationHandler(
                            IServiceProvider serviceProvider, 
                            IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        private void SetResponder()
        {
            //ESPERAR POR UMA CLASSE UsuarioRegistradoIntegrationEvent 
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarCliente(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso;

            //criar um scope, somente de metodo é um singleton
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}