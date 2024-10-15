using LojaManoel.Domain;
using LojaManoel.Service.Interface;

namespace LojaManoel.Service
{
    public class EmpacotarService : IEmpacotarService
    {
        private readonly List<Caixa> _caixasDisponiveis;

        public EmpacotarService()
        {
            _caixasDisponiveis = Util.Util.Caixas();
        }

        public List<(Caixa caixa, List<Produto> produtos)> Empacotar(Pedido pedido)
        {
            List<(Caixa caixa, List<Produto> produtos)> resultado = new List<(Caixa caixa, List<Produto> produtos)>();

            var produtosRestantes = new List<Produto>(pedido.Produtos);

            foreach (var caixa in _caixasDisponiveis.OrderBy(c => c.Volume))
            {
                List<Produto> produtosParaCaixa = new List<Produto>();
                
                bool pularProduto = false;

                foreach (var produto in produtosRestantes.OrderByDescending(c => c.Dimensoes.Volume).ToList())
                {

                    if (produto.Dimensoes.Altura <= caixa.Altura &&
                        produto.Dimensoes.Largura <= caixa.Largura && 
                        produto.Dimensoes.Comprimento <= caixa.Comprimento && 
                        produtosParaCaixa.Sum(p => p.Dimensoes.Volume) + produto.Dimensoes.Volume <= caixa.Volume &&
                        ProdutoCabeComRotacao(produto, caixa) &&
                        !pularProduto)
                    {
                        produtosParaCaixa.Add(produto);
                        produtosRestantes.Remove(produto);
                    }
                    else
                    {
                        pularProduto = true;
                    }
                }

                pularProduto = false;

                if (produtosParaCaixa.Any())
                {
                    resultado.Add((caixa, produtosParaCaixa));
                }

                if (!produtosRestantes.Any())
                {
                    break;
                }
            }

            if (produtosRestantes.Any())
            {
                resultado.Add((new Caixa { Caixa_Id = null, Observacao = "Produto não cabe em nenhuma caixa disponível." }, produtosRestantes));
            }

            return resultado;
        }

        public bool ProdutoCabeComRotacao(Produto produto, Caixa caixa)
        {
            return (produto.Dimensoes.Altura <= caixa.Altura && produto.Dimensoes.Largura <= caixa.Largura && produto.Dimensoes.Comprimento <= caixa.Comprimento) ||
                   (produto.Dimensoes.Altura <= caixa.Altura && produto.Dimensoes.Comprimento <= caixa.Largura && produto.Dimensoes.Largura <= caixa.Comprimento) ||
                   (produto.Dimensoes.Largura <= caixa.Altura && produto.Dimensoes.Altura <= caixa.Largura && produto.Dimensoes.Comprimento <= caixa.Comprimento) ||
                   (produto.Dimensoes.Largura <= caixa.Altura && produto.Dimensoes.Comprimento <= caixa.Largura && produto.Dimensoes.Altura <= caixa.Comprimento) ||
                   (produto.Dimensoes.Comprimento <= caixa.Altura && produto.Dimensoes.Altura <= caixa.Largura && produto.Dimensoes.Largura <= caixa.Comprimento) ||
                   (produto.Dimensoes.Comprimento <= caixa.Altura && produto.Dimensoes.Largura <= caixa.Largura && produto.Dimensoes.Altura <= caixa.Comprimento);
        }
    }
}
