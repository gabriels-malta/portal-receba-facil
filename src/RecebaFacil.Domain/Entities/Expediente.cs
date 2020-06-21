using RecebaFacil.Domain.Core.BaseEntities;
using RecebaFacil.Domain.Exception;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RecebaFacil.Domain.Entities
{
    public class Expediente : IEntityBase, IEquatable<Expediente>
    {
        public Guid Id { get; set; }
        public Guid PontoRetiradaId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraEncerramento { get; set; }

        public virtual PontoRetirada PontoRetirada { get; set; }

        public void ValidarHorarios()
        {
            if (HoraAbertura.CompareTo(HoraEncerramento) >= 0)
                throw new RecebaFacilException($"Horário de funcionamento inválido");
        }

        public void ValidarDiaSemana()
        {
            if (!Enum.IsDefined(typeof(DayOfWeek), DiaSemana))
                throw new RecebaFacilException("Dia da semana inválido");
        }

        public bool Equals([AllowNull] Expediente other)
        {
            if (other == null)
                return false;

            return PontoRetiradaId == other.PontoRetiradaId && DiaSemana == other.DiaSemana;
        }
    }
}
