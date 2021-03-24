using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SC.MessageBus;
using SC.Core.Utils;


namespace SC.Identidade.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
                
        }
    }
}