using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SC.WepApp.MVC.Extensions;
using System.Globalization;

namespace SC.WepApp.MVC.Configuration
{
    public static class WebAppConfig
    {
        public static void AddMvcConfiguration(this IServiceCollection services,IConfiguration configutarion)
        {
            services.AddControllersWithViews();
            //PEGAR AS CONFIGURAÇOES DO APP SER
            services.Configure<AppSettings>(configutarion);
        }
        public static void UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //COLOCAR O 500 PQ NÃO CONSEGUIU ENCONTRAR OU IDENTIFICAR O ERRO
                app.UseExceptionHandler("/erro/500");
                //é o erro do status code tratados
                app.UseStatusCodePagesWithRedirects("/erro/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityConfiguration();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            //centralizando todos os erros exceptions vai passar por aqui não precisa colocar em todo codigo
            app.UseMiddleware<ExceptionMiddleware>();
            //retirei pq esta na classe  app.UseIdentityConfiguration();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Catalogo}/{action=Index}/{id?}");
            });
        }
    }
}
