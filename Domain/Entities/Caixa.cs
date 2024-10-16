using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Caixa
    {
        public string CaixaId { get; private set; }
        public Dimensao Dimensao { get; private set; }

        public Caixa(string caixaId, Dimensao dimensao)
        {
            CaixaId = caixaId;
            Dimensao = dimensao;
        }
    }
}
