using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using System;

namespace RecebaFacil.Service
{
    public class ContatoService : IContatoService
    {
        private readonly IDataServiceContato _DataServiceContato;
        private readonly IEmpresaService _EmpresaService;
        private readonly ILogger<IContatoService> _logger;

        public ContatoService(IDataServiceContato dataServiceContato,
                              IEmpresaService empresaService, 
                              ILogger<IContatoService> logger)
        {
            _DataServiceContato = dataServiceContato;
            _EmpresaService = empresaService;
            _logger = logger;
        }

        public Contato ObterPorId(int id)
        {
            try
            {
                Contato contato = _DataServiceContato.ObterPorId(id);

                if (contato == null)
                    throw new RecebaFacilException("Contato não encontrado");

                return contato;
            }
            catch (Exception ex)
            {
                _logger.LogError("ContatoService.ObterPorId", ex.Message);
                    throw new RecebaFacilException("Contato não encontrado");
            }
        }

        public Contato ObterPorId(int id, bool carregarEmpresa)
        {
            try
            {
                Contato contato = ObterPorId(id);

                if (carregarEmpresa)
                    contato.AdicionarEmpresa(_EmpresaService.ObterPorId(contato.EmpresaID));

                return contato;
            }
            catch (Exception ex)
            {
                _logger.LogError("ContatoService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Contato não encontrado");
            }
        }

    }
}
