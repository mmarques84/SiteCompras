using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SC.WepApp.MVC.Configuration
{
    public static class IdentiyConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            //trabalhando com cookie para verificar se esta logado
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.AccessDeniedPath = "/acesso-negado";     

                });
        }
        public static void UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }


    }
}
