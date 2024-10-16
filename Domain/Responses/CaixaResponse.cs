using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class CaixaResponse
    {
        public string CaixaId { get; set; }
        public List<string> Produtos { get; set; } = new List<string>();
        public string Observacao { get; set; }
    }
}
