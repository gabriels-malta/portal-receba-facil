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
    public class ContatoService : IContatoService
    {
        private readonly IRepositoryContato _repositoryContato;
        private readonly ILogger<IContatoService> _logger;

        public ContatoService(IRepositoryContato repositoryContato,
                              ILogger<IContatoService> logger)
        {
            _repositoryContato = repositoryContato;
            _logger = logger;
        }

        public async Task Atualizar(Contato contato)
        {
            await _repositoryContato.Atualizar(contato);
        }

        public async Task Excluir(Guid id)
        {
            try
            {
                Contato contato = await _repositoryContato.ObterPorId(id);
                if (contato == null)
                    throw new RecebaFacilException("Contato inválido");

                await _repositoryContato.Excluir(contato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContatoService.Excluir", ex.Message);
                throw new RecebaFacilException("Erro ao excluir contato");
            }
        }

        public async Task<IList<Contato>> ListarPorEmpresa(Guid empresaId)
        {
            try
            {
                return await _repositoryContato.ObterListaPor(x => x.EmpresaId == empresaId);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContatoService.ListarPorEmpresa", ex.Message);
                throw new RecebaFacilException("Erro ao listar contatos");
            }
        }

        public async Task<Contato> ObterPorId(Guid id)
        {
            try
            {
                return await _repositoryContato.ObterPorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContatoService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Erro ao obter dados do contato");
            }
        }

        public async Task Salvar(Contato contato)
        {
            try
            {
                await _repositoryContato.Salvar(contato);
            }
            catch (Exception ex)
            {
                _logger.LogError("ContatoService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao salvar dados do contato");
            }
        }
    }
}
