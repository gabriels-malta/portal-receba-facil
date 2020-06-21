using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Service
{
    public class PreRegistroService : IPreRegistroService
    {
        private readonly IRepositoryPreRegistro _repositoryPreRegistro;

        public PreRegistroService(IRepositoryPreRegistro repositoryPreRegistro)
        {
            _repositoryPreRegistro = repositoryPreRegistro;
        }

        public async Task Salvar(PreRegistro preRegistro)
        {
            preRegistro.Id = Guid.NewGuid();
            await _repositoryPreRegistro.Salvar(preRegistro);
        }
    }
}
