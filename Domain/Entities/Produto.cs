using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Produto
    {
        public string ProdutoId { get; private set; }
        public Dimensao Dimensao { get; private set; }

        public Produto(string produtoId, Dimensao dimensao)
        {
            ProdutoId = produtoId;
            Dimensao = dimensao;
        }
    }
}
