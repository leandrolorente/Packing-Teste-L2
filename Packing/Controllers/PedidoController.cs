using Application.Commands.ProcessarPedido;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Packing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PedidoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Processa os pedidos e determina as melhores caixas para embalagem.
        /// </summary>
        [HttpPost("processar")]
        public async Task<IActionResult> ProcessarPedidos([FromBody] ProcessarPedidoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
