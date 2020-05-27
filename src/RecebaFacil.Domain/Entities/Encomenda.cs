using RecebaFacil.Domain.Exception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecebaFacil.Domain.Entities
{
    public class Encomenda : EntityBase<Guid>
    {
        public int PontoVendaId { get; set; }
        public int PontoRetiradaId { get; set; }
        public string NotaFiscal { get; set; }
        public string NumeroPedido { get; set; }
        public DateTime DataPedido { get; set; }

        private List<EncomendaHistoria> _historia = new List<EncomendaHistoria>();
        public IReadOnlyList<EncomendaHistoria> Historia => _historia;

        public void AdicionarHistoria(EncomendaHistoria historia)
        {
            if (_historia.Any(x => (x.EncomendaId, x.TipoMovimento.Id) == (historia.EncomendaId, historia.TipoMovimento.Id)))
                throw new RecebaFacilException("Movimento não permitido para esta encomendaa");

            _historia.Add(historia);
        }
    }
}
