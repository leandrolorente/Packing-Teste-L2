using Domain.Entities;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.ProcessarPedido
{
    public class ProcessarPedidoCommand : IRequest<ProcessarPedidoResponse>
    {
        public List<Pedido> Pedidos { get; set; }

        public ProcessarPedidoCommand(List<Pedido> pedidos)
        {
            Pedidos = pedidos;
        }
    }
}
