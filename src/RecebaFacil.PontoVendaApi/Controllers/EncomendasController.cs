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
    [Route("/api/ponto-venda/{empresaId}/encomendas")]
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
        public async Task<IActionResult> Get([FromRoute] string empresaId)
        {
            if (Guid.TryParse(empresaId, out Guid guidId))
            {
                var response = await _encomendaService.ObterPorPontoVenda(guidId);
                return Ok(response.Select(e => new EncomendaResponse(e.Id,
                                                                     e.DataPedido,
                                                                     e.NumeroPedido,
                                                                     e.NotaFiscal,
                                                                     e.Historia?.Select(h => new EncomendaHistoriaResponse(h.DataCadastro, h.TipoMovimento.GetDescription()))
                                                                     )
                ));
            }

            return BadRequest();
        }

        [HttpGet("{encomendaId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EncomendaResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] string empresaId,
                                                 [FromRoute] Guid encomendaId)
        {
            if (Guid.TryParse(empresaId, out Guid guidId))
            {
                var response = await _encomendaService.ObterPorId(encomendaId);
                if (response == null)
                {
                    _logger.LogWarning($"Encomenda {encomendaId} nao encontrada");
                    return BadRequest();
                }

                return Ok(new EncomendaResponse
                (
                    response.Id,
                    response.DataPedido,
                    response.NumeroPedido,
                    response.NotaFiscal,
                    response.Historia?.Select(h => new EncomendaHistoriaResponse(h.DataCadastro, h.TipoMovimento.GetDescription()))
                ));
            }
            return BadRequest();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromRoute] string empresaId,
                                              [FromBody] EncomendaRequest model)
        {
            try
            {
                var response = await _encomendaService.Salvar(new Domain.Entities.Encomenda
                {
                    DataPedido = model.DataPedido,
                    NotaFiscal = model.NotaFiscal,
                    NumeroPedido = model.NumeroPedido,
                    PontoRetiradaId = model.PontoRetiradaId,
                    PontoVendaId = Guid.Parse(empresaId)
                });
                return CreatedAtAction(nameof(GetById), "PontoVendaEncomendas", new
                {
                    empresaId,
                    encomendaId = response
                }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Erro ao registrar nova encomenda");
            }
        }

        [HttpPatch("{encomendaId:guid}/movimentar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Patch([FromRoute] string empresaId, [FromRoute] Guid encomendaId)
        {
            try
            {
                await _encomendaService.MovimentarPorPontoVenda(encomendaId, Guid.Parse(empresaId));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Erro ao movimentar encomenda");
            }
        }
    }
}
