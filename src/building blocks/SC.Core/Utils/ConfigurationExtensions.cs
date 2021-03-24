using Microsoft.Extensions.Configuration;

namespace SC.Core.Utils
{
    //SIMILAR AO GETCONECTIOSTRING
    public static class ConfigurationExtensions
    {
        public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
        {
            //MessageQueueConnection NOME DA SESSAO
            return configuration?.GetSection("MessageQueueConnection")?[name];
        }
    }
}