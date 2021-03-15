using Microsoft.AspNetCore.Mvc;
using SC.Clientes.API.Application.Commands;
using SC.Core.Mediator;
using SC.WebApi.Core.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC.Clientes.API.Controllers
{
    public class ClientesController : MainController
    {

        private readonly IMediatorHandler _mediatorHandler;

        public ClientesController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler=mediatorHandler;
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> IndexAsync()
        {
            var resultado=await _mediatorHandler.EnviarComando
                (new RegistrarClienteCommand(Guid.NewGuid(), "marcus", "marcus@teste.com", "02096235510"));

            return CustomResponse(resultado);
        }
    }
}
