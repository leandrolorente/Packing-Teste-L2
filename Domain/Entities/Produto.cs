using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Produto
    {
        
        public string Produto_id { get; private set; }
        
        public Dimensao Dimensoes { get; private set; }

        public Produto(string produto_id, Dimensao dimensoes)
        {
            Produto_id = produto_id;
            Dimensoes = dimensoes;
        }
    }
}
