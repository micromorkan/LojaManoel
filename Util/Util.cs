using LojaManoel.Domain;

namespace LojaManoel.Util
{
    public static class Util
    {
        public static List<Caixa> Caixas()
        {
            return new List<Caixa>
            {
                new Caixa { Caixa_Id = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
                new Caixa { Caixa_Id = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
                new Caixa { Caixa_Id = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            };
        }
    }
}
