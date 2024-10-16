using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class ProcessarPedidoResponse
    {
        public List<ResultadoPedido> Pedidos { get; set; } = new List<ResultadoPedido>();
    }

    public class ResultadoPedido
    {
        public int PedidoId { get; set; }
        public List<ResultadoCaixa> Caixas { get; set; } = new List<ResultadoCaixa>();
    }

    public class ResultadoCaixa
    {
        public string CaixaId { get; set; }
        public List<string> Produtos { get; set; } = new List<string>();
        public string Observacao { get; set; }
    }
}
