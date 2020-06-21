using System.ComponentModel;

namespace RecebaFacil.Domain.Entities
{
    public enum TipoMovimento
    {
        [Description("Esteira iniciada")]
        EsteiraIniciada = 1,

        [Description("Enviado para Ponto de Retirada")]
        EnviadoPontoRetirada = 2,

        [Description("Recebido pelo Ponto de Retirada")]
        RecebidoPontoRetirada = 3,

        [Description("Aguardando cliente final")]
        AguardandoClienteFinal = 4,

        [Description("Retirado pelo cliente final")]
        RetiradoClienteFinal = 5,

        [Description("Esteira finalizada")]
        EsteiraFinalizada = 6,


        [Description("Aguardando cliente final a 7 dias")]
        AguardandoClienteFinal7Dias = 50,

        [Description("Aguardando cliente final a 14 dias")]
        AguardandoClienteFinal14Dias = 51,

        [Description("Devoldido para Ponto de Venda")]
        DevolvidoPontoVenda = 52,


        [Description("Número do pedido alterada")]
        NumeroPedidoAlterado = 200,

        [Description("Nota Fiscal alterada")]
        NotaFiscalAlterada = 201

    }
}
