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
    public class EnderecoService : IEnderedecoService
    {
        private readonly IRepositoryEndereco _repositoryEndereco;
        private readonly ILogger<IEnderedecoService> _logger;

        public EnderecoService(
            IRepositoryEndereco repositoryEndereco,
            ILogger<IEnderedecoService> logger)
        {
            _repositoryEndereco = repositoryEndereco;
            _logger = logger;
        }

        public async Task<Endereco> ObterAtivoPorEmpresa(Guid empresaId)
        {
            try
            {
                return await _repositoryEndereco.ObterPrimeiroPor(x => x.EmpresaId == empresaId && x.Ativo);
            }
            catch (Exception ex)
            {

                _logger.LogError("EnderecoService.ObterAtivoPorEmpresa", ex.Message);
                throw new RecebaFacilException("Endereço não encontrado");
            }
        }

        public async Task<IList<Endereco>> ObterPorEmpresa(Guid empresaId)
        {
            try
            {
                return await _repositoryEndereco.ObterListaPor(x => x.EmpresaId == empresaId);
            }
            catch (Exception ex)
            {
                _logger.LogError("EnderecoService.ObterPorEmpresa", ex.Message);
                throw new RecebaFacilException("Endereço não encontrado");
            }
        }

        public async Task<Endereco> ObterPorId(Guid id)
        {
            try
            {
                return await _repositoryEndereco.ObterPorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("EnderecoService.ObterAtivoPorEmpresa", ex.Message);
                throw new RecebaFacilException("Endereço não encontrado");
            }
        }

        public async Task Salvar(Endereco endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco.Cep) || endereco.Cep.Length > 8)
                throw new RecebaFacilException("CEP inválida");

            if (string.IsNullOrWhiteSpace(endereco.Uf) || endereco.Uf.Length != 2)
                throw new RecebaFacilException("UF inválida");

            endereco.Id = Guid.NewGuid();
            try
            {
                await _repositoryEndereco.Salvar(endereco);
            }
            catch (Exception ex)
            {
                _logger.LogError("EnderecoService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao salvar o endereço");
            }
        }
    }
}
