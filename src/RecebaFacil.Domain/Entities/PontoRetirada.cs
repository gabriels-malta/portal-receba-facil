using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Exception;
using System.Collections.Generic;
using System.Linq;

namespace RecebaFacil.Domain.Entities
{
    public class PontoRetirada : Empresa
    {
        public override TipoEmpresa TipoEmpresa => TipoEmpresa.PontoRetirada;        
        public IReadOnlyList<Expediente> Expediente => _expediente;

        private List<Expediente> _expediente = new List<Expediente>();

        public void AdicionarExpediente(Expediente expediente)
        {
            if (_expediente.Any(x => x.DiaSemana == expediente.DiaSemana))
                throw new RecebaFacilException("Já existe um expediente para esse dia da semana");

            _expediente.Add(expediente);
        }
    }
}
