
using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RecebaFacil.Domain.Services
{
    public interface IExpedienteService
    {
        bool Salvar(Expediente expediente);
        bool Alterar(Expediente expediente);
        Expediente ObterPorId(Guid id);
        IEnumerable<Expediente> ObterPorEmpresa(int empresaId);
    }
}
