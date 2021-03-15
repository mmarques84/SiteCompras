using System;
using FluentValidation.Results;
using MediatR;

namespace SC.Core.Messages
{
    //essa abstração serve para ser um comando que sera chamado nas api: registrarCliente
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime Timestamp { get; private set; }
        //FluentValidation: é um objeto que ele possui uma lista de erro, para validar os erros nas dos comandos.
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            //porque ele é virtual , pode querer dar override, não consegui validar uma coisa que não é valida
            throw new NotImplementedException();
        }
    }
}