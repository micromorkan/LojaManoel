namespace LojaManoel.Domain
{
    public class Pedido
    {
        public int Pedido_Id { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
