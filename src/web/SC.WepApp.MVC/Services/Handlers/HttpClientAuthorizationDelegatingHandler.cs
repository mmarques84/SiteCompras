using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using SC.WebApp.MVC;
using SC.WebApp.MVC.Extensions;
using SC.WepApp.MVC.Extensions;

namespace SC.WebApp.MVC.Services.Handlers
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IUser _user;

        public HttpClientAuthorizationDelegatingHandler(IUser user)
        {
            _user = user;
        }
        //FAZER A VALIDAÇÃO DO TOKEN E AUTENTICACAO
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });
            }

            var token = _user.ObterUserToken();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}