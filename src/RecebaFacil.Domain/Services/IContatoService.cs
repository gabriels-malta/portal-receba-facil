
using RecebaFacil.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Domain.Services
{
    public interface IContatoService
    {
        Task Atualizar(Contato contato);
        Task Salvar(Contato contato);
        Task<Contato> ObterPorId(Guid id);
        Task Excluir(Guid id);
        Task<IList<Contato>> ListarPorEmpresa(Guid empresaId);
    }
}
