using Domain.Interfaces;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProcessarPedido
{
    public class ProcessarPedidoHandler : IRequestHandler<ProcessarPedidoCommand, ProcessarPedidoResponse>
    {
        private readonly IPedidoPackingService _pedidoPackingService;

        public ProcessarPedidoHandler(IPedidoPackingService pedidoPackingService)
        {
            _pedidoPackingService = pedidoPackingService;
        }

        public async Task<ProcessarPedidoResponse> Handle(ProcessarPedidoCommand request, CancellationToken cancellationToken)
        {
            var domainResponse = await _pedidoPackingService.ProcessarPedidosAsync(request.Pedidos);

            var applicationResponse = new ProcessarPedidoResponse
            {
                Pedidos = domainResponse.Pedidos.Select(p => new ResultadoPedido
                {
                    PedidoId = p.PedidoId,
                    Caixas = p.Caixas.Select(c => new ResultadoCaixa
                    {
                        CaixaId = c.CaixaId,
                        Produtos = c.Produtos,
                        Observacao = c.Observacao
                    }).ToList()
                }).ToList()
            };

            return applicationResponse;
        }
    }
}
