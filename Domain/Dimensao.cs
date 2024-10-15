namespace LojaManoel.Domain
{
    public class Dimensao
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
        public int Volume => Altura * Largura * Comprimento;
    }
}
