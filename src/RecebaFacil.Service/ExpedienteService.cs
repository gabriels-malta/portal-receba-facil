using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using System;
using System.Collections.Generic;

namespace RecebaFacil.Service
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IDataServiceExpediente _DataServiceExpediente;
        private readonly ILogger<IExpedienteService> _logger;

        public ExpedienteService(IDataServiceExpediente dataServiceExpediente,
                                 ILogger<IExpedienteService> logger)
        {
            _DataServiceExpediente = dataServiceExpediente;
            _logger = logger;
        }

        public bool Alterar(Expediente expediente)
        {
            try
            {
                return _DataServiceExpediente.Alterar(expediente) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.Alterar", ex.Message);
                throw new RecebaFacilException("Erro ao alterar dados do expediente");
            }
        }

        public IEnumerable<Expediente> ObterPorEmpresa(int empresaId)
        {
            try
            {
                return _DataServiceExpediente.ObterPorEmpresa(empresaId);
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.ObterPorEmpresa", ex.Message);
                throw new RecebaFacilException("Empresa sem expediente");
            }
        }

        public Expediente ObterPorId(Guid id)
        {
            try
            {
                return _DataServiceExpediente.ObterPorId(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Expediente não encontrado");
            }
        }

        public bool Salvar(Expediente expediente)
        {
            try
            {
                return _DataServiceExpediente.Salvar(expediente) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError("ExpedienteService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao gravar dados do expediente");
            }
        }
    }
}
