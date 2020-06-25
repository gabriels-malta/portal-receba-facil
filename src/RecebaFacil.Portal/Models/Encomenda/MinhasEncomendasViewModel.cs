using System;

namespace RecebaFacil.Portal.Models.Encomenda
{
    public class MinhasEncomendasViewModel
    {
        public Guid Id { get; set; }
        public Guid PontoVendaId { get; set; }
        public string NotaFiscal { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime DataPedido { get; set; }
        public string TipoMovimento { get; internal set; }
        public string PontoVendaNome { get; set; }
    }
}
