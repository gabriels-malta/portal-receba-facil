namespace RecebaFacil.Domain.Entities
{
    public enum TipoMovimento
    {
        EsteiraIniciada = 1,
        EnviadoPontoRetirada = 2,
        RecebidoPontoRetirada = 3,
        AguardandoClienteFinal = 4,
        RetiradoClienteFinal = 5,
        EsteiraFinalizada = 6,

        AguardandoClienteFinal7Dias = 50,
        AguardandoClienteFinal14Dias = 51,
        DevolvidoPontoVenda = 52,

        NumeroPedidoAlterado = 200,
        NotaFiscalAlterada = 201

    }
}
