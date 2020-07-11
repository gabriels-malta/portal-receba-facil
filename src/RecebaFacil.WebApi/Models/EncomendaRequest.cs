using System;
using System.ComponentModel.DataAnnotations;

namespace RecebaFacil.WebApi.Models
{
    public struct EncomendaRequest
    {
        public EncomendaRequest(Guid pontoRetiradaId,
                                string numeroPedido,
                                string notaFiscal,
                                DateTime dataPedido)
        {
            PontoRetiradaId = pontoRetiradaId;
            NumeroPedido = numeroPedido;
            NotaFiscal = notaFiscal;
            DataPedido = dataPedido;
        }

        [Required]
        public Guid PontoRetiradaId { get; private set; }

        [Required]
        public string NumeroPedido { get; private set; }

        [Required]
        public string NotaFiscal { get; private set; }

        [Required]
        public DateTime DataPedido { get; private set; }
    }
}
