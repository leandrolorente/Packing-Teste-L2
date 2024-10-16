using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class PedidoResponse
    {
        public int Pedido_id { get; set; }
        public List<CaixaResponse> Caixas { get; set; } = new List<CaixaResponse>();
    }
}
