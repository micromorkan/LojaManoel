using LojaManoel.Domain;

namespace LojaManoel.Service.Interface
{
    public interface IEmpacotarService
    {
        List<(Caixa caixa, List<Produto> produtos)> Empacotar(Pedido pedido);
    }
}
