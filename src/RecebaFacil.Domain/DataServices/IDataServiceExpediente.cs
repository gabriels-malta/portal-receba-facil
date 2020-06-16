using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceExpediente
    {
        Expediente ObterPorId(Guid id);
        int Salvar(Expediente expediente);
        IEnumerable<Expediente> ObterPorEmpresa(int empresaId);
        int Alterar(Expediente expediente);

    }
}
