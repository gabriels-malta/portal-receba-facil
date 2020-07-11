
using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IEmpresaService
    {
        Task<string> ObterNomeEmpresa(Guid empresaId);
        Task<bool> ExistePontoVenda(Guid id);
        Task<bool> ExistePontoRetirada(Guid id);
        Task<Empresa> ObterPorId(Guid id);

        Task<IList<Empresa>> ObterPontosVenda();
        Task<IList<Empresa>> ObterPontosRetirada();
    }
}
