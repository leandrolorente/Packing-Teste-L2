using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido
    {
        
        public int Pedido_id { get; private set; }
        
        public List<Produto> Produtos { get; private set; }

        public Pedido(int pedido_id, List<Produto> produtos)
        {
            Pedido_id = pedido_id;
            Produtos = produtos;
        }
    }
}
