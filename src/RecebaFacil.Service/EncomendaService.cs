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
                await ValidarNovaEncomenda(encomenda);

                encomenda.CriarNovaHistoria(TipoMovimento.EsteiraIniciada);

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
            var encomenda = await _repositoryEncomenda.ObterPorId(encomendaId);

            TipoMovimento proximaEtapa = EncomendaHistoria.DefinirProximoMovimento(encomenda.ObterEstadoAtual());

            await ValidarMovimentacao(encomendaId, pontoVendaId, proximaEtapa, encomenda);

            var historia = encomenda.CriarNovaHistoria(proximaEtapa);

            await _repositoryHistoria.Salvar(historia);
        }
        public async Task AdicionarMovimento(Guid encomendaId, Guid empresaId, TipoMovimento movimento)
        {
            var encomenda = await _repositoryEncomenda.ObterPorId(encomendaId);

            await ValidarMovimentacao(encomendaId, empresaId, movimento, encomenda);

            var historia = encomenda.CriarNovaHistoria(movimento);
            await _repositoryHistoria.Salvar(historia);

            if (TipoMovimento.RecebidoPontoRetirada == movimento)
            {
                var historiaAguardandoClienteFinal = encomenda.CriarNovaHistoria(TipoMovimento.AguardandoClienteFinal);
                await _repositoryHistoria.Salvar(historiaAguardandoClienteFinal);
            }

            if (!encomenda.PodeMovimentar())
            {
                var historiaFinal = encomenda.CriarNovaHistoria(TipoMovimento.EsteiraFinalizada);
                await _repositoryHistoria.Salvar(historiaFinal);
            }

        }
        public async Task<Encomenda> ObterPorId(Guid id)
        {
            return await _repositoryEncomenda.ObterPorId(id);
        }
        public async Task Despachar(Guid pontoVendaId, Guid encomendaId, int notaFiscal)
        {
            var encomenda = await _repositoryEncomenda.ObterPorId(encomendaId);

            await ValidarEnvioParaPontoDeRetirada(pontoVendaId, encomendaId, notaFiscal, encomenda);

            encomenda.NotaFiscal = notaFiscal.ToString();
            await _repositoryEncomenda.Atualizar(encomenda);

            var historiaNF = encomenda.CriarNovaHistoria(TipoMovimento.NotaFiscalAlterada);
            await _repositoryHistoria.Salvar(historiaNF);

            var historiaEnviado = encomenda.CriarNovaHistoria(TipoMovimento.EnviadoPontoRetirada);
            await _repositoryHistoria.Salvar(historiaEnviado);
        }
        private async Task ValidarNovaEncomenda(Encomenda encomenda)
        {
            if (!await _empresaService.ExistePontoRetirada(encomenda.PontoRetiradaId))
                throw new RecebaFacilException("Ponto de Retirada inválido");

            if (!await _empresaService.ExistePontoVenda(encomenda.PontoVendaId))
                throw new RecebaFacilException("Ponto de Venda inválido");

            if (DateTime.Now.CompareTo(encomenda.DataPedido) == 1)
                throw new RecebaFacilException("Data do pedido inválida");

            if (string.IsNullOrWhiteSpace(encomenda.NumeroPedido))
                throw new RecebaFacilException("Número do pedido é obrigatório");

            if (await _repositoryEncomenda.Existe(x => x.NumeroPedido == encomenda.NumeroPedido))
                throw new RecebaFacilException($"Já existe uma encomenda com este número de pedido: {encomenda.NumeroPedido}");
        }
        private async Task ValidarMovimentacao(Guid encomendaId, Guid empresaId, TipoMovimento movimento, Encomenda encomenda)
        {
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
        }
        private async Task ValidarEnvioParaPontoDeRetirada(Guid pontoVendaId, Guid encomendaId, int notaFiscal, Encomenda encomenda)
        {
            if (encomenda == null)
            {
                _logger.LogWarning($"Data: ${DateTime.Now} | EncomendaId: ${encomendaId} | EmpresaId: ${pontoVendaId}");
                throw new RecebaFacilException("Encomenda não encontrada");
            }
            if (!new int[] { 1, 999_999_999 }.Contains(notaFiscal))
            {
                _logger.LogWarning($"Data: ${DateTime.Now} | EmpresaId: ${pontoVendaId} | Nota Fiscal inválida");
                throw new RecebaFacilException("Númeração de nota fiscal inválida");
            }
            if (!await _empresaService.ExistePontoVenda(pontoVendaId))
            {
                _logger.LogWarning($"Data: ${DateTime.Now} | Empresa inválida (EmpresaId: ${pontoVendaId})");
                throw new RecebaFacilException("Ação não permitida");
            }
        }
    }
}
