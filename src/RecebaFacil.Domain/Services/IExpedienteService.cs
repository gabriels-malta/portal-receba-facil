
using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IExpedienteService
    {
        Task Salvar(Expediente expediente);
        Task<Expediente> ObterPorId(Guid id);
        Task<IList<Expediente>> ObterPorEmpresa(Guid empresaId);
        Task Excluir(Guid id);
    }
}
