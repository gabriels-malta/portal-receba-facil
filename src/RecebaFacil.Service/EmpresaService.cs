using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IRepositoryEmpresa _repositoryEmpresa;
        private readonly ILogger<IEmpresaService> _logger;

        public EmpresaService(IRepositoryEmpresa repositoryEmpresa,
                              ILogger<IEmpresaService> logger)
        {
            _repositoryEmpresa = repositoryEmpresa;
            _logger = logger;
        }

        public async Task<Empresa> ObterPorId(Guid id)
        {
            try
            {
                return await _repositoryEmpresa.ObterPorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("EmpresaService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Empresa não encontrada");
            }
        }
    }
}
