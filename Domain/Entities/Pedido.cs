using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido
    {
        public int PedidoId { get; private set; }
        public List<Produto> Produtos { get; private set; }

        public Pedido(int pedidoId, List<Produto> produtos)
        {
            PedidoId = pedidoId;
            Produtos = produtos;
        }
    }
}
