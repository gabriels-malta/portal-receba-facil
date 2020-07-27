using System;

namespace RecebaFacil.Portal.Models.Encomenda
{
    public class EncomendaHistoriaViewModel
    {
        public Guid EncomendaId { get; set; }
        public string Nome { get; internal set; }
        public DateTime DataMovimento { get; internal set; }
    }
}
