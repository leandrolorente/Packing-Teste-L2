﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class ProcessarPedidoResponse
    {
        public List<PedidoResponse> Pedidos { get; set; } = new List<PedidoResponse>();
    }

    
}
