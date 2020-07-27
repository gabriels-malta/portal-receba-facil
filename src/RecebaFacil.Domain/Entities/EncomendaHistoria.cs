using RecebaFacil.Domain.Core.BaseEntities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Exception;
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

        public bool Equals([AllowNull] EncomendaHistoria other)
        {
            if (other == null)
                return false;

            return EncomendaId == other.EncomendaId && TipoMovimento == other.TipoMovimento;
        }

        public void DefinirProximoMovimento(TipoMovimento movimentoAtual)
        {
            TipoMovimento = movimentoAtual switch
            {
                TipoMovimento.EsteiraIniciada => TipoMovimento.EnviadoPontoRetirada,
                TipoMovimento.EnviadoPontoRetirada => TipoMovimento.RecebidoPontoRetirada,
                TipoMovimento.RecebidoPontoRetirada => TipoMovimento.AguardandoClienteFinal,
                TipoMovimento.AguardandoClienteFinal => TipoMovimento.RetiradoClienteFinal,
                TipoMovimento.RetiradoClienteFinal => TipoMovimento.EsteiraFinalizada,
                _ => throw new RecebaFacilException("Movimento inválido"),
            };
        }
    }
}
