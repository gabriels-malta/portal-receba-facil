using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecebaFacil.Service
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IRepositoryExpediente _repositoryExpediente;
        private readonly ILogger<IExpedienteService> _logger;

        public ExpedienteService(IRepositoryExpediente repositoryExpediente,
                                 ILogger<IExpedienteService> logger)
        {
            _repositoryExpediente = repositoryExpediente;
            _logger = logger;
        }

        public async Task Excluir(Guid id)
        {
            try
            {
                Expediente expediente = await _repositoryExpediente.ObterPorId(id);
                if (expediente == null)
                    throw new RecebaFacilException("Expediente não encontrado");

                await _repositoryExpediente.Excluir(expediente);
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.Excluir", ex.Message);
                throw new RecebaFacilException("Erro ao exluir expediente");
            }
        }

        public async Task<IList<Expediente>> ObterPorEmpresa(Guid empresaId)
        {
            try
            {
                return await _repositoryExpediente.ObterListaPor(x => x.PontoRetiradaId == empresaId);
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.ObterPorEmpresa", ex.Message);
                throw new RecebaFacilException("Erro ao obter expedientes do ponto de retirada");
            }
        }

        public async Task<Expediente> ObterPorId(Guid id)
        {
            try
            {
                return await _repositoryExpediente.ObterPorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Erro ao obter expediente");
            }
        }

        public async Task Salvar(Expediente expediente)
        {
            try
            {
                IList<Expediente> expedientes = await _repositoryExpediente.ObterListaPor(x => x.PontoRetiradaId == expediente.PontoRetiradaId);

                // validar se existe um expediente com dia da semana e horários iguais
                if (expedientes.Any(x => x.Equals(expediente)))
                    throw new RecebaFacilException("Já existe um expediente para este dia da semana");

                if (Guid.Empty.Equals(expediente.Id))
                {
                    // não permitir salvar um dia da semana repetido
                    if (expedientes.Any(x => x.DiaSemana == expediente.DiaSemana))
                        throw new RecebaFacilException("Já existe um expediente para este dia da semana");

                    await _repositoryExpediente.Salvar(expediente);
                }
                else
                    await _repositoryExpediente.Atualizar(expediente);
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao salvar expediente");
            }
        }
    }
}
