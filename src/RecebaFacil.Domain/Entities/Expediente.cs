using System;

namespace RecebaFacil.Domain.Entities
{
    public class Expediente : EntityBase<Guid>
    {
        public int PontoRetiradaID { get; set; }
        public byte DiaSemana { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraEncerramento { get; set; }
    }
}
