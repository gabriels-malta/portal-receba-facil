using System;
using System.Collections.Generic;

namespace RecebaFacil.WebApi.Models
{
    public struct EncomendaResponse
    {
        public EncomendaResponse(Guid id,
                                 DateTime dataPedido,
                                 string numeroPedido,
                                 string notaFiscal,
                                 IEnumerable<EncomendaHistoriaResponse> historia)
        {
            Id = id;
            DataPedido = dataPedido;
            NumeroPedido = numeroPedido;
            NotaFiscal = notaFiscal;
            Historia = historia;
        }

        public Guid Id { get; private set; }
        public DateTime DataPedido { get; private set; }
        public string NumeroPedido { get; private set; }
        public string NotaFiscal { get; private set; }
        public IEnumerable<EncomendaHistoriaResponse> Historia { get; private set; }

    }
}
