using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using SC.Clientes.API.Application.Events;
using SC.Clientes.API.Models;
using SC.Core.Messages;

namespace SC.Clientes.API.Application.Commands
{
    //sera o mbaseanipular do commad validar os commands e pode persistir na 
    public class ClienteCommandHandler : CommandHandler,//class para usar o validationresult 
        IRequestHandler<RegistrarClienteCommand, ValidationResult>
        //vai manipular o manipulado do resquest
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        //cancellationToken controlar a thares
        public async Task<ValidationResult> Handle(RegistrarClienteCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido()) return message.ValidationResult;

            var cliente = new Cliente(message.Id, message.Nome, message.Email, message.Cpf,false);

            var clienteExistente = await _clienteRepository.ObterPorCpf(cliente.Cpf.Numero);

            if (clienteExistente != null)
            {
                AdicionarErro("Este CPF já está em uso.");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);
            //lancar o evento apos a criação, ele vai tratar os if e entidades
            cliente.AdicionarEvento(new ClienteRegistradoEvent(message.Id, message.Nome, message.Email, message.Cpf));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}