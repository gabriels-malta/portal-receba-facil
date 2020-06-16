using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using System;

namespace RecebaFacil.Service
{
    public class EnderecoService : IEnderedecoService
    {
        private readonly IDataServiceEndereco _DataServiceEndereco;
        private readonly ILogger<IEnderedecoService> _logger;

        public EnderecoService(IDataServiceEndereco dataServiceEndereco, 
            ILogger<IEnderedecoService> logger)
        {
            _DataServiceEndereco = dataServiceEndereco;
            _logger = logger;
        }

        public Endereco ObterPorId(int id)
        {
            try
            {
                return _DataServiceEndereco.ObterPorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("EnderecoService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Endereço não encontrado");
            }
        }

        public Endereco ObterPorEmpresa(int empresaId)
        {
            try
            {
                return _DataServiceEndereco.ObterEnderecoPorEmpresa(empresaId, somentePrincipal: true);
            }
            catch (Exception ex)
            {
                _logger.LogError("EnderecoService.ObterPorEmpresa", ex.Message);
                throw new RecebaFacilException("Endereço não encontrado");
            }
        }

        public int Salvar(Endereco endereco)
        {
            if (endereco.EmpresaId < 0)
                throw new RecebaFacilException("Empresa inválida");

            if (string.IsNullOrWhiteSpace(endereco.Cep) || endereco.Cep.Length > 8)
                throw new RecebaFacilException("CEP inválida");

            if (string.IsNullOrWhiteSpace(endereco.Uf) || endereco.Uf.Length != 2)
                throw new RecebaFacilException("UF inválida");

            try
            {
                return _DataServiceEndereco.Salvar(endereco);
            }
            catch (Exception ex)
            {
                _logger.LogError("EnderecoService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao salvar o endereço");
            }
        }
    }
}
