using System;
using Microsoft.Extensions.DependencyInjection;

namespace SC.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        //
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();
            //trabalhar schema singleton, criamos a connection string em qualquer api que vai consumir
            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}