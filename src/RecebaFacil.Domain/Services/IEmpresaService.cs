
using RecebaFacil.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IEmpresaService
    {
        Task<Empresa> ObterPorId(Guid id);
    }
}
