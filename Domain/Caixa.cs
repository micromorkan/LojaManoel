namespace LojaManoel.Domain
{
    public class Caixa
    {
        public string Caixa_Id { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }
        public decimal Volume => Altura * Largura * Comprimento;
        public string Observacao { get; set; }
    }
}
