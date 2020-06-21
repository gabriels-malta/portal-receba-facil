using RecebaFacil.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IGrupoService
    {
        Task<Grupo> ObterPorId(Guid id);
    }
}
