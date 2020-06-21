
using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IEnderedecoService
    {
        Task<Endereco> ObterPorId(Guid id);
        Task<IList<Endereco>> ObterPorEmpresa(Guid empresaId);
        Task Salvar(Endereco endereco);

        Task<Endereco> ObterAtivoPorEmpresa(Guid empresaId);
    }
}
