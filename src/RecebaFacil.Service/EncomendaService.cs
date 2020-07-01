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
    public class EncomendaService : IEncomendaService
    {
        private readonly ILogger<IEncomendaService> _logger;
        private readonly IRepositoryEncomenda _repositoryEncomenda;
        private readonly IRepositoryEncomendaHistoria _repositoryHistoria;

        private readonly IEmpresaService _empresaService;
        public EncomendaService(IRepositoryEncomenda repositoryEncomenda,
                                IEmpresaService empresaService,
                                ILogger<IEncomendaService> logger,
                                IRepositoryEncomendaHistoria repositoryHistoria)
        {
            _repositoryEncomenda = repositoryEncomenda;
            _empresaService = empresaService;
            _logger = logger;
            _repositoryHistoria = repositoryHistoria;
        }

        public async Task<IList<Encomenda>> ObterPorPontoVenda(Guid pontoVendaId)
        {
            return await _repositoryEncomenda.ObterListaPor(x => x.PontoVendaId == pontoVendaId);
        }
        public async Task<IList<Encomenda>> ObterPorPontoDeRetirada(Guid pontoRetiradaId)
        {
            return await _repositoryEncomenda.ObterListaPor(x => x.PontoRetiradaId == pontoRetiradaId);
        }

        public async Task Salvar(Encomenda encomenda)
        {
            try
            {
                if (!await _empresaService.Existe(encomenda.PontoRetiradaId))
                    throw new RecebaFacilException("Ponto de Retirada inválido");

                if (!await _empresaService.Existe(encomenda.PontoVendaId))
                    throw new RecebaFacilException("Ponto de Venda inválido");

                if (string.IsNullOrWhiteSpace(encomenda.NumeroPedido) || string.IsNullOrWhiteSpace(encomenda.NotaFiscal))
                    throw new RecebaFacilException("Verifique os dados e tente novamente");

                encomenda.AdicionarHistoria(new EncomendaHistoria
                {
                    Id = Guid.NewGuid(),
                    EncomendaId = encomenda.Id,
                    DataCadastro = DateTime.UtcNow,
                    TipoMovimento = TipoMovimento.EsteiraIniciada
                });

                await _repositoryEncomenda.Salvar(encomenda);
            }
            catch (Exception ex)
            {
                _logger.LogError("RecebaFacil.Service.EncomendaService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao cadastrar nova encomenda");
            }
        }

        public async Task Movimentar(EncomendaHistoria historia)
        {
            if (!await _repositoryEncomenda.Existe(x => x.Id == historia.EncomendaId))
                throw new RecebaFacilException("Encomenda não encccontrada");

            IList<EncomendaHistoria> movimentos = await _repositoryHistoria.ObterListaPor(x => x.EncomendaId == historia.EncomendaId);

            TipoMovimento ultimoMovimento = movimentos.Max(x => x.TipoMovimento);

            if (historia.TipoMovimento.CompareTo(ultimoMovimento) < 0)
                throw new RecebaFacilException("Movimento inválido");

            if (movimentos.Any(x => x.Equals(historia)))
                throw new RecebaFacilException("Movimento já cadastrado");

            historia.Id = Guid.NewGuid();
            await _repositoryHistoria.Salvar(historia);
        }
    }
}
