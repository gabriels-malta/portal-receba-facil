using System.ComponentModel;

namespace RecebaFacil.Domain.Enums
{
    public enum TipoEmpresa
    {
        SemDados = 0,

        [Description("Ponto de Venda")]
        PontoVenda = 1,

        [Description("Ponto de Retirada")]
        PontoRetirada = 2,
    }
}
