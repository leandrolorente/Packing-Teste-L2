using Application.Commands.ProcessarPedido;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoPackingService _pedidoPackingService;

        public PedidoService(IPedidoPackingService pedidoPackingService)
        {
            _pedidoPackingService = pedidoPackingService;
        }

        public Task<Pedido> CriarPedidoAsync(List<Produto> produtos)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido> ObterPedidoPorIdAsync(int pedido_id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pedido>> ObterTodosOsPedidosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProcessarPedidoResponse> ProcessarPedidoAsync(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public async Task<ProcessarPedidoResponse> ProcessarPedidos(List<Pedido> pedidos)
        {
            return await _pedidoPackingService.ProcessarPedidosAsync(pedidos);
        }
    }
}
