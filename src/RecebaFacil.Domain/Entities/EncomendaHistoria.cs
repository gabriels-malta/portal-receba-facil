using System;

namespace RecebaFacil.Domain.Entities
{
    public class EncomendaHistoria : EntityBase<Guid>
    {
        public Guid EncomendaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public int UsuarioId { get; set; }
        public string Observacao { get; set; }
    }
}
