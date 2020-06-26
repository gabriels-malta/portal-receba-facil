using System;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class ExpedienteViewModel
    {
        public Guid Id { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraEncerramento { get; set; }
        private string _diaSemana;

        public string DiaSemana
        {
            get
            {
                return (Enum.Parse<DayOfWeek>(_diaSemana)) switch
                {
                    DayOfWeek.Sunday => "Domingo",
                    DayOfWeek.Monday => "Segunda-feira",
                    DayOfWeek.Tuesday => "Terça-feira",
                    DayOfWeek.Wednesday => "Quarta-feira",
                    DayOfWeek.Thursday => "Quinta-feira",
                    DayOfWeek.Friday => "Sexta-feira",
                    DayOfWeek.Saturday => "Sábado",
                    _ => "-",
                };
            }
            set { _diaSemana = value; }
        }

    }
}
