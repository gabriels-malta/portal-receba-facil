using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<string> ObterNomeEmpresa(Guid empresaId)
        {
            var empresa = await _repositoryEmpresa.ObterPorId(empresaId);
            if (empresa != null)
                return empresa.RazaoSocial;

            return string.Empty;
        }

        public async Task<bool> ExistePontoVenda(Guid id) => await _repositoryEmpresa.Existe(x => x.Id == id && x.TipoEmpresa == Domain.Enums.TipoEmpresa.PontoVenda);
        public async Task<bool> ExistePontoRetirada(Guid id) => await _repositoryEmpresa.Existe(x => x.Id == id && x.TipoEmpresa == Domain.Enums.TipoEmpresa.PontoRetirada);


        public async Task<Empresa> ObterPorId(Guid id)
        {
            try
            {
                Empresa empresa = await _repositoryEmpresa.ObterPorId(id);

                return empresa;
            }
            catch (Exception ex)
            {
                _logger.LogError("EmpresaService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Empresa não encontrada");
            }
        }

        public async Task<IList<Empresa>> ObterPontosVenda()
        {
            try
            {
                return await _repositoryEmpresa.ObterListaPor(x => x.TipoEmpresa == Domain.Enums.TipoEmpresa.PontoVenda);
            }
            catch (Exception ex)
            {
                _logger.LogError("EmpresaService.ObterPontosVenda", ex.Message);
                throw new RecebaFacilException("Nenhum ponto de venda cadastrado");
            }
        }

        public async Task<IList<Empresa>> ObterPontosRetirada()
        {
            try
            {
                return await _repositoryEmpresa.ObterListaPor(x => x.TipoEmpresa == Domain.Enums.TipoEmpresa.PontoRetirada);
            }
            catch (Exception ex)
            {
                _logger.LogError("EmpresaService.ObterPontosRetirada", ex.Message);
                throw new RecebaFacilException("Nenhum ponto de retirada cadastrado");
            }
        }
    }
}
