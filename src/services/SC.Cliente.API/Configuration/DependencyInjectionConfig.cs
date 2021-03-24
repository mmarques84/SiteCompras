using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SC.Clientes.API.Application.Commands;
using SC.Clientes.API.Application.Events;
using SC.Clientes.API.Data;
using SC.Clientes.API.Data.Repository;
using SC.Clientes.API.Models;
using SC.Clientes.API.Services;
using SC.Core.Mediator;

namespace SC.Clientes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //adicionar os escopos que serão utilizados no codigos
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();
            //um objeto singloton, 
           // services.AddHostedService<RegistroClienteIntegrationHandler>();
        }
    }
}