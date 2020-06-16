using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using System;

namespace RecebaFacil.Service
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IDataServiceEmpresa _DataServiceEmpresa;
        private readonly IEnderedecoService _EnderecoService;
        private readonly ILogger<IEmpresaService> _logger;

        public EmpresaService(IDataServiceEmpresa dataServiceEmpresa,
                              IEnderedecoService enderecoService,
                              ILogger<IEmpresaService> logger)
        {
            _DataServiceEmpresa = dataServiceEmpresa;
            _EnderecoService = enderecoService;
            _logger = logger;
        }

        public Empresa ObterPorId(int id)
        {
            try
            {
                Empresa empresa = _DataServiceEmpresa.ObterPorId(id);

                if (empresa == null)
                    throw new RecebaFacilException("Empresa não encontrada");

                return empresa;
            }
            catch (Exception ex)
            {
                _logger.LogError("EmpresaService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Empresa não encontrada");
            }
        }

        public Empresa ObterPorId(int id, bool carregarEndereco)
        {
            try
            {
                Empresa empresa = ObterPorId(id);

                if (carregarEndereco)
                    empresa.AdicionarEndereco(_EnderecoService.ObterPorEmpresa(empresaId: id));

                return empresa;
            }
            catch (Exception ex)
            {
                _logger.LogError("EmpresaService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Empresa não encontrada");
            }
        }
    }
}
