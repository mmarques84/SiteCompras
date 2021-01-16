using Microsoft.Extensions.Options;
using SC.WepApp.MVC.Extensions;
using SC.WepApp.MVC.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SC.WepApp.MVC.Services
{
    public class AutenticacaoService : Service,IAutenticacaoService
    {
        private readonly HttpClient _httpClient;
      

        public AutenticacaoService(HttpClient httpClient,
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoURL);
            _httpClient = httpClient;
         
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin usuarioLogin)
        {
            var loginContent = ObterstringContent(usuarioLogin);
            
            var response =await _httpClient.PostAsync("/api/identidade/autenticar", loginContent);
         
            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                   ResponseResult= await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }

            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response);


        }

        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro usuarioRegistro)
        {
           
            var registroContent = ObterstringContent(usuarioRegistro);
            var response = await _httpClient.PostAsync("/api/identidade/novo-usuario", registroContent);
          
            if (!TratarErrosResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }
            return await DeserializarObjetoResponse<UsuarioRespostaLogin>(response); 
        }
    }
}
