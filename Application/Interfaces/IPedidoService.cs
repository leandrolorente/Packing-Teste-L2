using Domain.Entities;
using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPedidoService
    {
        /// <summary>
        /// Cria um novo pedido com os produtos especificados.
        /// </summary>
        /// <param name="produtos">A lista de produtos para o pedido.</param>
        /// <returns>O pedido criado.</returns>
        Task<Pedido> CriarPedidoAsync(List<Produto> produtos);

        /// <summary>
        /// Processa o pedido, embalando os produtos em caixas.
        /// </summary>
        /// <param name="pedido">O pedido a ser processado.</param>
        /// <returns>O resultado do processamento do pedido.</returns>
        Task<ProcessarPedidoResponse> ProcessarPedidoAsync(Pedido pedido);

        /// <summary>
        /// Obtém uma lista de todos os pedidos.
        /// </summary>
        /// <returns>Uma lista de pedidos.</returns>
        Task<List<Pedido>> ObterTodosOsPedidosAsync();

        /// <summary>
        /// Obtém detalhes de um pedido específico.
        /// </summary>
        /// <param name="pedidoId">O ID do pedido.</param>
        /// <returns>Os detalhes do pedido.</returns>
        Task<Pedido> ObterPedidoPorIdAsync(int pedidoId);
    }
}
