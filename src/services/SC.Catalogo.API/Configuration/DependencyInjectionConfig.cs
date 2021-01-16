using Microsoft.Extensions.DependencyInjection;
using SC.Catalogo.API.Data;
using SC.Catalogo.API.Data.Repository;
using SC.Catalogo.API.Models;

namespace SC.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<CatalogoContext>();
        }
    }
}