using Domain.Entities;
using Domain.Interfaces;
using Domain.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class PedidoPackingService : IPedidoPackingService
    {
        // Definição das caixas pela capacidade em volume (altura * largura * comprimento)
        private readonly Dictionary<TipoCaixa, (double Altura, double Largura, double Comprimento)> _caixas = new Dictionary<TipoCaixa, (double, double, double)>
        {
            { TipoCaixa.Caixa1, (30, 40, 80) },
            { TipoCaixa.Caixa2, (80, 50, 40) },
            { TipoCaixa.Caixa3, (50, 80, 60) }
        };

        /// <summary>
        /// Processa uma lista de pedidos e faz o empacotamento dos produtos em caixas disponíveis.
        /// </summary>
        /// <param name="pedidos">Lista de pedidos a serem empacotados.</param>
        /// <returns>Retorna uma resposta com os produtos empacotados por caixa.</returns>
        public async Task<ProcessarPedidoResponse> ProcessarPedidosAsync(List<Pedido> pedidos)
        {
            var response = new ProcessarPedidoResponse();

            foreach (var pedido in pedidos)
            {
                var pedidoResponse = new PedidoResponse { Pedido_id = pedido.Pedido_id };

                // Agrupa os produtos em caixas
                foreach (var produto in pedido.Produtos)
                {
                    var caixaId = EmpacotarProduto(produto);
                    if (!string.IsNullOrEmpty(caixaId))
                    {
                        AdicionarProdutoNaCaixa(pedidoResponse, caixaId, produto.Produto_id);
                    }
                    else
                    {
                        // Produto não cabe em nenhuma caixa
                        pedidoResponse.Caixas.Add(new CaixaResponse
                        {
                            CaixaId = null,
                            Produtos = new List<string> { produto.Produto_id },
                            Observacao = "Produto não cabe em nenhuma caixa disponível."
                        });
                    }
                }

                response.Pedidos.Add(pedidoResponse);
            }

            return await Task.FromResult(response);
        }

        /// <summary>
        /// Empacota o produto na caixa mais adequada com base nas dimensões.
        /// </summary>
        /// <param name="produto">Produto a ser empacotado.</param>
        /// <returns>O identificador da caixa onde o produto foi empacotado ou null se o produto não couber.</returns>
        private string EmpacotarProduto(Produto produto)
        {
            // Tenta encontrar a caixa que comporte o produto
            foreach (var caixa in _caixas)
            {
                if (ProdutoCabeNaCaixa(produto.Dimensoes, caixa.Value))
                {
                    return caixa.Key.ToString();
                }
            }

            // Se o produto não caber em nenhuma caixa
            return null;
        }

        /// <summary>
        /// Verifica se o produto cabe na caixa especificada.
        /// </summary>
        /// <param name="dimensoesProduto">Dimensões do produto.</param>
        /// <param name="dimensoesCaixa">Dimensões da caixa.</param>
        /// <returns>True se o produto couber na caixa; caso contrário, False.</returns>
        private bool ProdutoCabeNaCaixa(Dimensao dimensoesProduto, (double Altura, double Largura, double Comprimento) dimensoesCaixa)
        {
            // Verifica se o produto cabe dentro da caixa, considerando todas as orientações possíveis
            return (dimensoesProduto.Altura <= dimensoesCaixa.Altura &&
                    dimensoesProduto.Largura <= dimensoesCaixa.Largura &&
                    dimensoesProduto.Comprimento <= dimensoesCaixa.Comprimento) ||
                   (dimensoesProduto.Altura <= dimensoesCaixa.Largura &&
                    dimensoesProduto.Largura <= dimensoesCaixa.Comprimento &&
                    dimensoesProduto.Comprimento <= dimensoesCaixa.Altura) ||
                   (dimensoesProduto.Altura <= dimensoesCaixa.Comprimento &&
                    dimensoesProduto.Largura <= dimensoesCaixa.Altura &&
                    dimensoesProduto.Comprimento <= dimensoesCaixa.Largura);
        }

        /// <summary>
        /// Adiciona o produto na caixa do pedido correspondente.
        /// </summary>
        /// <param name="pedidoResponse">Resposta do pedido.</param>
        /// <param name="caixaId">Identificador da caixa.</param>
        /// <param name="produtoId">Identificador do produto.</param>
        private void AdicionarProdutoNaCaixa(PedidoResponse pedidoResponse, string caixaId, string produtoId)
        {
            var caixa = pedidoResponse.Caixas.FirstOrDefault(c => c.CaixaId == caixaId);
            if (caixa == null)
            {
                caixa = new CaixaResponse { CaixaId = caixaId };
                pedidoResponse.Caixas.Add(caixa);
            }

            caixa.Produtos.Add(produtoId);
        }
    }
}