using RecebaFacil.Domain.Core.BaseEntities;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class EncomendaEmpresa : IEntityBase
    {
        public Guid EncomendaId { get; set; }
        public int PontoVendaId { get; set; }
        public int PontoRetiradaId { get; set; }
        public Guid Id { get; set; }
    }
}
