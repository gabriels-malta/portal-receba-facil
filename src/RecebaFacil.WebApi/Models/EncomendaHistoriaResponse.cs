using Newtonsoft.Json;
using System;

namespace RecebaFacil.WebApi.Models
{
    public struct EncomendaHistoriaResponse
    {
        public EncomendaHistoriaResponse(DateTime dataMovimento,
                                         string movimento)
        {
            DataMovimento = dataMovimento;
            Movimento = movimento;
        }

        public DateTime DataMovimento { get; private set; }
        public string Movimento { get; private set; }
    }
}
