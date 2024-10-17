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
        private readonly Dictionary<TipoCaixa, (double Altura, double Largura, double Comprimento)> _caixas = new Dictionary<TipoCaixa, (double, double, double)>
        {
            { TipoCaixa.Caixa1, (30, 40, 80) },
            { TipoCaixa.Caixa2, (80, 50, 40) },
            { TipoCaixa.Caixa3, (50, 80, 60) }
        };

        public async Task<ProcessarPedidoResponse> ProcessarPedidosAsync(List<Pedido> pedidos)
        {
            var response = new ProcessarPedidoResponse();

            foreach (var pedido in pedidos)
            {
                var pedidoResponse = new PedidoResponse { Pedido_id = pedido.Pedido_id };
                var produtosOrdenados = pedido.Produtos.OrderByDescending(p => CalcularVolume(p.Dimensoes.Altura, p.Dimensoes.Largura, p.Dimensoes.Comprimento)).ToList();

                foreach (var produto in produtosOrdenados)
                {
                    var caixaId = EmpacotarProduto(pedidoResponse, produto, pedidos);
                    if (!string.IsNullOrEmpty(caixaId))
                    {
                        AdicionarProdutoNaCaixa(pedidoResponse, caixaId, produto.Produto_id);
                    }
                    else
                    {
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

        private string EmpacotarProduto(PedidoResponse pedidoResponse, Produto produto, List<Pedido> pedidos)
        {
            foreach (var caixa in _caixas.OrderBy(c => CalcularVolume(c.Value.Altura, c.Value.Largura, c.Value.Comprimento)))
            {
                var caixaExistente = pedidoResponse.Caixas.FirstOrDefault(c => c.CaixaId == caixa.Key.ToString() && ProdutoCabeNaCaixa(produto, caixa.Value, c, pedidos));

                if (caixaExistente != null)
                {
                    return caixaExistente.CaixaId;
                }

                if (ProdutoCabeNaCaixa(produto, caixa.Value, null, pedidos))
                {
                    return caixa.Key.ToString();
                }
            }
            return null;
        }

        private bool ProdutoCabeNaCaixa(Produto produto, (double Altura, double Largura, double Comprimento) dimensoesCaixa, CaixaResponse caixaExistente, List<Pedido> pedidos)
        {
            double volumeDisponivel = CalcularVolume(dimensoesCaixa.Altura, dimensoesCaixa.Largura, dimensoesCaixa.Comprimento);
            double volumeProduto = CalcularVolume(produto.Dimensoes.Altura, produto.Dimensoes.Largura, produto.Dimensoes.Comprimento);

            double alturaMaxOcupada = 0;
            double larguraTotalOcupada = 0;
            double comprimentoTotalOcupado = 0;

            if (caixaExistente != null)
            {
                foreach (var produtoId in caixaExistente.Produtos)
                {
                    var produtoExistente = ObterProdutoPorId(pedidos, produtoId);
                    volumeDisponivel -= CalcularVolume(produtoExistente.Dimensoes.Altura, produtoExistente.Dimensoes.Largura, produtoExistente.Dimensoes.Comprimento);

                    alturaMaxOcupada = Math.Max(alturaMaxOcupada, produtoExistente.Dimensoes.Altura);
                    larguraTotalOcupada += produtoExistente.Dimensoes.Largura;
                    comprimentoTotalOcupado = Math.Max(comprimentoTotalOcupado, produtoExistente.Dimensoes.Comprimento);
                }
            }

            if (volumeProduto > volumeDisponivel)
            {
                return false;
            }

            return ProdutoCabeNasDimensoes(produto.Dimensoes, dimensoesCaixa) &&
                   VerificarOrientacao(produto.Dimensoes.Altura, produto.Dimensoes.Largura, produto.Dimensoes.Comprimento,
                                       dimensoesCaixa, comprimentoTotalOcupado, larguraTotalOcupada, alturaMaxOcupada);
        }

        private bool ProdutoCabeNasDimensoes(Dimensao produtoDimensoes, (double Altura, double Largura, double Comprimento) caixaDimensoes)
        {
            return (produtoDimensoes.Altura <= caixaDimensoes.Altura && produtoDimensoes.Largura <= caixaDimensoes.Largura && produtoDimensoes.Comprimento <= caixaDimensoes.Comprimento) ||
                   (produtoDimensoes.Altura <= caixaDimensoes.Altura && produtoDimensoes.Largura <= caixaDimensoes.Comprimento && produtoDimensoes.Comprimento <= caixaDimensoes.Largura) ||
                   (produtoDimensoes.Altura <= caixaDimensoes.Largura && produtoDimensoes.Largura <= caixaDimensoes.Altura && produtoDimensoes.Comprimento <= caixaDimensoes.Comprimento) ||
                   (produtoDimensoes.Altura <= caixaDimensoes.Largura && produtoDimensoes.Largura <= caixaDimensoes.Comprimento && produtoDimensoes.Comprimento <= caixaDimensoes.Altura) ||
                   (produtoDimensoes.Altura <= caixaDimensoes.Comprimento && produtoDimensoes.Largura <= caixaDimensoes.Altura && produtoDimensoes.Comprimento <= caixaDimensoes.Largura) ||
                   (produtoDimensoes.Altura <= caixaDimensoes.Comprimento && produtoDimensoes.Largura <= caixaDimensoes.Largura && produtoDimensoes.Comprimento <= caixaDimensoes.Altura);
        }

        private bool VerificarOrientacao(double alturaProduto, double larguraProduto, double comprimentoProduto, (double Altura, double Largura, double Comprimento) dimensoesCaixa, double comprimentoTotalOcupado, double larguraTotalOcupada, double alturaMaxOcupada)
        {
            bool cabeLadoALado = alturaProduto <= dimensoesCaixa.Altura &&
                                 larguraTotalOcupada + larguraProduto <= dimensoesCaixa.Largura &&
                                 comprimentoTotalOcupado <= dimensoesCaixa.Comprimento;

            bool cabeEmpilhado = alturaMaxOcupada + alturaProduto <= dimensoesCaixa.Altura &&
                                 larguraProduto <= dimensoesCaixa.Largura &&
                                 comprimentoTotalOcupado <= dimensoesCaixa.Comprimento;

            bool cabeRotacionado = alturaProduto <= dimensoesCaixa.Altura &&
                                   larguraTotalOcupada + comprimentoProduto <= dimensoesCaixa.Largura &&
                                   larguraProduto <= dimensoesCaixa.Comprimento;

            bool cabeRotacionadoEmpilhado = alturaMaxOcupada + alturaProduto <= dimensoesCaixa.Altura &&
                                            comprimentoProduto <= dimensoesCaixa.Largura &&
                                            larguraProduto <= dimensoesCaixa.Comprimento;

            return cabeLadoALado || cabeEmpilhado || cabeRotacionado || cabeRotacionadoEmpilhado;
        }

        private double CalcularVolume(double altura, double largura, double comprimento)
        {
            return altura * largura * comprimento;
        }

        private Produto ObterProdutoPorId(List<Pedido> pedidos, string produtoId)
        {
            foreach (var pedido in pedidos)
            {
                var produtoEncontrado = pedido.Produtos.FirstOrDefault(p => p.Produto_id == produtoId);
                if (produtoEncontrado != null)
                {
                    return produtoEncontrado;
                }
            }
            return null;
        }

        private void AdicionarProdutoNaCaixa(PedidoResponse pedidoResponse, string caixaId, string produtoId)
        {
            var caixa = pedidoResponse.Caixas.FirstOrDefault(c => c.CaixaId == caixaId);
            if (caixa == null)
            {
                caixa = new CaixaResponse { CaixaId = caixaId, Produtos = new List<string>() };
                pedidoResponse.Caixas.Add(caixa);
            }

            caixa.Produtos.Add(produtoId);
        }
    }
}