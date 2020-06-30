using System;

namespace RecebaFacil.Portal.Models.PontoRetirada
{
    public class ExpedienteViewModel
    {
        public Guid Id { get; set; }
        public string HoraAbertura { get; set; }
        public string HoraEncerramento { get; set; }
        public string DiaSemana { get; set; }

    }
}
