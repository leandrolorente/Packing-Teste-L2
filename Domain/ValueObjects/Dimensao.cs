using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Dimensao
    {
        public class Dimensao
        {
            public double Altura { get; private set; }
            public double Largura { get; private set; }
            public double Comprimento { get; private set; }

            public Dimensao(double altura, double largura, double comprimento)
            {
                Altura = altura;
                Largura = largura;
                Comprimento = comprimento;
            }
        }
    }
}
