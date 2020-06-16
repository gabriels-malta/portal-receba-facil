using RecebaFacil.Domain.Exception;
using System;

namespace RecebaFacil.Domain.Entities
{
    public class Expediente : EntityBase<Guid>
    {
        private Expediente() { }

        public Expediente(Guid id,
            int pontoRetiradaID,
            int diaSemana,
            TimeSpan horaAbertura,
            TimeSpan horaEncerramento)
        {
            Id = id;
            PontoRetiradaID = pontoRetiradaID;
            DiaSemana = diaSemana;
            HoraAbertura = horaAbertura;
            HoraEncerramento = horaEncerramento;

            ValidarDiaSemana();
            ValidarHorarios();
        }

        public Expediente(
            int pontoRetiradaID,
            int diaSemana,
            TimeSpan horaAbertura,
            TimeSpan horaEncerramento)
        {
            Id = Guid.NewGuid();
            PontoRetiradaID = pontoRetiradaID;
            DiaSemana = diaSemana;
            HoraAbertura = horaAbertura;
            HoraEncerramento = horaEncerramento;

            ValidarDiaSemana();
            ValidarHorarios();
        }

        public int PontoRetiradaID { get; private set; }
        public int DiaSemana { get; private set; }
        public TimeSpan HoraAbertura { get; private set; }
        public TimeSpan HoraEncerramento { get; private set; }

        private void ValidarHorarios()
        {
            if (HoraAbertura.CompareTo(HoraEncerramento) >= 0)
                throw new RecebaFacilException($"Horário de funcionamento inválido");
        }

        private void ValidarDiaSemana()
        {
            if (!Enum.IsDefined(typeof(DayOfWeek), DiaSemana))
                throw new RecebaFacilException("Dia da semana inválido");
        }
    }
}
