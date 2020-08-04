using System;
using System.ComponentModel.DataAnnotations;

namespace RecebaFacil.WebApi.Models
{
    public struct EncomendaNFRequest
    {
        [Required]
        public Guid EncomendaId { get; private set; }
        [Required]
        public string NotaFiscal { get; private set; }
    }
}
