using System;

namespace RecebaFacil.Portal.Models.Encomenda
{
    public class MinhasEncomendasViewModel
    {
        public Guid Id { get; internal set; }
        public Guid PontoVendaId { get; internal set; }
        public string NotaFiscal { get; internal set; }
        public string NumeroPedido { get; internal set; }
        public DateTime DataPedido { get; internal set; }
        public string TipoMovimento { get; internal set; }
        public string PontoVendaNome { get; internal set; }
        public string UrlEncomendaDetalhe { get; internal set; }
    }
}
