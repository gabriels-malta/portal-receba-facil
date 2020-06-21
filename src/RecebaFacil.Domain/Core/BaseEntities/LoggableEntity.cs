using System;

namespace RecebaFacil.Domain.Core.BaseEntities
{
    public abstract class LoggableEntity : IEntityBase
    {
        public DateTime DataCadastro { get; set; }
        public DateTime DataUltimaModificacao { get; set; }
        public Guid Id { get; set; }
    }
}
