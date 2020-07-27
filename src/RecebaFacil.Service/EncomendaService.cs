using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
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

        public async Task<Guid> Salvar(Encomenda encomenda)
        {
            try
            {
                if (!await _empresaService.ExistePontoRetirada(encomenda.PontoRetiradaId))
                    throw new RecebaFacilException("Ponto de Retirada inválido");

                if (!await _empresaService.ExistePontoVenda(encomenda.PontoVendaId))
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

                return await _repositoryEncomenda.Salvar(encomenda);
            }
            catch (Exception ex)
            {
                _logger.LogError("RecebaFacil.Service.EncomendaService.Salvar", ex.Message);
                throw new RecebaFacilException("Erro ao cadastrar nova encomenda");
            }
        }

        public async Task MovimentarPorPontoVenda(Guid encomendaId, Guid pontoVendaId)
        {
            if (!await _repositoryEncomenda.Existe(x => x.PontoVendaId == pontoVendaId))
                throw new RecebaFacilException("Operação inválida para este ponto de venda");

            IList<EncomendaHistoria> movimentos = await _repositoryHistoria.ObterListaPor(x => x.EncomendaId == encomendaId);

            if (!movimentos.Any()) throw new RecebaFacilException("Encomenda não encontrada");

            EncomendaHistoria historia = new EncomendaHistoria
            {
                Id = Guid.NewGuid(),
                EncomendaId = encomendaId,
                DataCadastro = DateTime.Now
            };

            historia.DefinirProximoMovimento(movimentoAtual: movimentos.ElementAt(0).TipoMovimento);

            await _repositoryHistoria.Salvar(historia);
        }

        public async Task AdicionarMovimento(Guid encomendaId, Guid empresaId, TipoMovimento movimento)
        {
            var encomenda = await _repositoryEncomenda.ObterPorId(encomendaId);
            if (encomenda == null)
            {
                _logger.LogWarning($"Data: ${DateTime.Now} | EncomendaId: ${encomendaId} | EmpresaId: ${empresaId}");
                throw new RecebaFacilException("Encomenda não encontrada");
            }

            if (!encomenda.PodeMovimentar())
            {
                _logger.LogWarning($"Data: ${DateTime.Now} | EncomendaId: ${encomendaId} | EmpresaId: ${empresaId} | Movimento pretendido: ${movimento}");
                throw new RecebaFacilException("Situação atual não permite movimentação");
            }

            var empresa = await _empresaService.ObterPorId(empresaId);
            if (!encomenda.ObterMovimentosPermitidos(empresa.TipoEmpresa).Contains(movimento))
            {
                _logger.LogWarning($"Data: ${DateTime.Now} | EncomendaId: ${encomendaId} | EmpresaId: ${empresaId} | Movimento pretendido: ${movimento}");
                throw new RecebaFacilException("Movimentação inválida");
            }

            _ = await _repositoryHistoria.Salvar(new EncomendaHistoria
            {
                Id = Guid.NewGuid(),
                DataCadastro = DateTime.Now,
                EncomendaId = encomendaId,
                TipoMovimento = movimento
            });
        }
        
        public async Task<Encomenda> ObterPorId(Guid id)
        {
            return await _repositoryEncomenda.ObterPorId(id);
        }
    }
}
