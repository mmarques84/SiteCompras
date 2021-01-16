using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SC.WebApp.MVC.Services;
using SC.WepApp.MVC.Extensions;
using SC.WepApp.MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.WepApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();
            services.AddHttpClient<ICatalogoService, CatalogoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//porque singleton, porque tenho que estudar 
            
            services.AddScoped<IUser, AspNetUser>();//adicionar via scoped, fica limitado ao request
        }
    }
}
