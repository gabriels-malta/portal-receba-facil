using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Services;
using RecebaFacil.WebApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecebaFacil.WebApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/ponto-venda/{empresaId:guid}/encomendas")]
    public class EncomendasController : ControllerBase
    {
        private readonly IEncomendaService _encomendaService;
        private readonly ILogger<EncomendasController> _logger;
        public EncomendasController(IEncomendaService encomendaService,
                                              ILogger<EncomendasController> logger)
        {
            _encomendaService = encomendaService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EncomendaResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid empresaId)
        {
            try
            {
                var encomendas = await _encomendaService.ObterPorPontoVenda(empresaId);

                if (!encomendas.Any())
                {
                    _logger.LogWarning($"Sem encomendas cadastradas para o Ponto de Retirada Id: ${empresaId}");
                    return NotFound();
                }

                var response = encomendas.Select(e => new EncomendaResponse(e.Id,
                                                                      e.DataPedido,
                                                                      e.NumeroPedido,
                                                                      e.NotaFiscal,
                                                                      e.GetHistoria()?.Select(h => new EncomendaHistoriaResponse(h.DataCadastro, h.TipoMovimento.GetDescription()))
                                                                      )
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Erro ao obter encomendas");
            }
        }

        [HttpGet("{encomendaId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EncomendaResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid encomendaId)
        {
            try
            {
                var response = await _encomendaService.ObterPorId(encomendaId);
                if (response == null)
                {
                    _logger.LogWarning($"Encomenda {encomendaId} nao encontrada");
                    return NotFound();
                }

                return Ok(new EncomendaResponse
                (
                    response.Id,
                    response.DataPedido,
                    response.NumeroPedido,
                    response.NotaFiscal,
                    response.GetHistoria()?.Select(h => new EncomendaHistoriaResponse(h.DataCadastro, h.TipoMovimento.GetDescription()))
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GravarNovoPost([FromRoute] Guid empresaId,
                                                        [FromBody] EncomendaRequest model)
        {
            try
            {
                var response = await _encomendaService.Salvar(new Domain.Entities.Encomenda
                {
                    DataPedido = model.DataPedido,
                    NotaFiscal = null,
                    NumeroPedido = model.NumeroPedido,
                    PontoRetiradaId = model.PontoRetiradaId,
                    PontoVendaId = empresaId
                });
                return CreatedAtAction("GetById", "Encomendas", new
                {
                    empresaId,
                    encomendaId = response
                }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{encomendaId:guid}/movimentar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MovimentarPost([FromRoute] Guid empresaId,
                                                        [FromRoute] Guid encomendaId)
        {
            try
            {
                await _encomendaService.MovimentarPorPontoVenda(encomendaId, empresaId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{encomendaId:guid}/despachar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DespacharPatch([FromRoute] Guid empresaId,
                                                        [FromRoute] Guid encomendaId,
                                                        [FromBody] int notaFiscal)
        {
            try
            {
                await _encomendaService.Despachar(empresaId, encomendaId, notaFiscal);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
