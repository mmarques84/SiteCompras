using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.Clientes.API.Services;
using SC.Core.Utils;
using SC.MessageBus;

namespace SC.Clientes.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            //PEGAR A CONNECTIO STRING, instancia o service hospedado
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistroClienteIntegrationHandler>();
        }
    }
}