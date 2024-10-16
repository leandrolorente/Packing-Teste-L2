using Domain.Entities;
using Domain.Interfaces;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PedidoPackingService : IPedidoPackingService
    {
        public async Task<ProcessarPedidoResponse> ProcessarPedidosAsync(List<Pedido> pedidos)
        {
            var response = new ProcessarPedidoResponse();

            // Lógica de empacotamento

            return await Task.FromResult(response);
        }
    }
}
