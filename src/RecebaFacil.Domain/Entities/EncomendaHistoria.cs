using RecebaFacil.Domain.Core.BaseEntities;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RecebaFacil.Domain.Entities
{
    public class EncomendaHistoria : IEntityBase, IEquatable<EncomendaHistoria>
    {
        public Guid Id { get; set; }
        public Guid EncomendaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public int UsuarioId { get; set; }
        public string Observacao { get; set; }

        public bool Equals([AllowNull] EncomendaHistoria other)
        {
            if (other == null)
                return false;

            return EncomendaId == other.EncomendaId && TipoMovimento == other.TipoMovimento;
        }
    }
}
