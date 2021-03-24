    using System;

namespace SC.Core.Messages.Integration
{
    //pode ser usando em outra api, por essse motivo essa mensagem que vai
    public class UsuarioRegistradoIntegrationEvent: IntegrationEvent
    {
        //
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string  Cpf { get; private set; }
        public UsuarioRegistradoIntegrationEvent(Guid id, string nome, string email, string cpf, bool excluido)
        {
            Id = id;
            Nome = nome;
            Email =  (email);//validar o exception da DomainObjects
            Cpf =  (cpf);
   
            //Endereco = endereco;

        }
    }
}
