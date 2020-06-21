using System;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class ExpedienteViewModel
    {
        public Guid Id { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraEncerramento { get; set; }
        public DayOfWeek DiaSemana { get; set; }
    }
}
