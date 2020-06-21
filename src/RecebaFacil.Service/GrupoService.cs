using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Service
{
    public class GrupoService : IGrupoService
    {
        private readonly IRepositoryGrupo _repositoryGrupo;

        public GrupoService(IRepositoryGrupo repositoryGrupo)
        {
            _repositoryGrupo = repositoryGrupo;
        }

        public async Task<Grupo> ObterPorId(Guid id)
        {
            return await _repositoryGrupo.ObterPorId(id);
        }
    }
}
