using Domain.Entities;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPedidoPackingService
    {
        Task<ProcessarPedidoResponse> ProcessarPedidosAsync(List<Pedido> pedidos);
    }
}
