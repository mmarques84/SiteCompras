using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using SC.WebApp.MVC.Extensions;
using SC.WebApp.MVC.Services;
using SC.WebApp.MVC.Services.Handlers;
using SC.WepApp.MVC.Extensions;
using SC.WepApp.MVC.Services;
using System;
using System.Net.Http;

namespace SC.WepApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>(); // pq trasiente, pq chama uma estancia de cada vez
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<ICatalogoService, CatalogoService>()
               .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
               //.AddTransientHttpErrorPolicy(
               //p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
               .AddPolicyHandler(PollyExtensions.EsperarTentar())
               .AddTransientHttpErrorPolicy(
                   p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//porque singleton, porque tenho que estudar 
            
            services.AddScoped<IUser, AspNetUser>();//adicionar via scoped, fica limitado ao request

            #region Refit

            //services.AddHttpClient("Refit",
            //        options =>
            //        {
            //            options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
            //        })
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

            #endregion
        }

        public class PollyExtensions
        {
            public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
            {
                var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    }, (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retryCount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

                return retry;
            }
        }
    }
}
