﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models.Encomenda;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize]
    [Route("encomenda")]
    public class EncomendaController : BaseWithCacheController
    {
        private readonly IEmpresaService _empresaService;
        private readonly IEncomendaService _encomendaService;
        public EncomendaController(IHttpContextService httpContextService,
                                   ICacheService cacheService,
                                   IEncomendaService encomendaService,
                                   IEmpresaService empresaService)
            : base(httpContextService, cacheService)
        {
            _encomendaService = encomendaService;
            _empresaService = empresaService;
        }

        #region ::: PONTO DE VENDA :::
        [Authorize(Roles = Roles.PONTO_VENDA)]
        [Route("ponto-venda/ver-encomendas", Name = "Encomenda_PontoVenda_Index")]
        public async Task<IActionResult> PontoVendaIndex()
        {
            ViewBag.NovaEncomedaURL = Url.RouteUrl("Encomenda_PontoVenda_Nova");
            IList<EncomendaViewModel> model = new List<EncomendaViewModel>();

            var encomendas = await _encomendaService.ObterPorPontoVenda(_loggedUser.EmpresaId);

            foreach (var item in encomendas)
            {
                string nomePontoRetirada = await _empresaService.ObterNomeEmpresa(item.PontoRetiradaId);

                model.Add(new EncomendaViewModel
                {
                    Id = item.Id,
                    DataPedido = item.DataPedido,
                    NotaFiscal = item.NotaFiscal,
                    NumeroPedido = item.NumeroPedido,
                    PontoRetiradaId = item.PontoRetiradaId,
                    PontoVendaId = item.PontoVendaId,
                    TipoMovimento = item.GetHistoria().Max(x => x.TipoMovimento).GetDescription(),
                    PontoRetiradaNome = nomePontoRetirada
                });
            }

            return PartialView(model);
        }

        [Authorize(Roles = Roles.PONTO_VENDA)]
        [Route("ponto-venda/nova-encomenda", Name = "Encomenda_PontoVenda_Nova")]
        public async Task<IActionResult> NovaEncomenda()
        {
            var pontosRetirada = await _empresaService.ObterPontosRetirada();

            NovaEncomendaViewModel model = new NovaEncomendaViewModel
            {
                ListaPontoRetirada = pontosRetirada.Select(p => new SelectListItem(p.NomeFantasia, p.Id.ToString()))
            };

            return PartialView("_CadastroEncomenda", model);
        }

        [Authorize(Roles = Roles.PONTO_VENDA)]
        [HttpPost("ponto-venda/nova-encomenda", Name = "Encomenda_CadastrarNova")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CadastrarNovaEncomenda(NovaEncomendaViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("_CadastroEncomenda", model);

                await _encomendaService.Salvar(new Encomenda
                {
                    PontoVendaId = _loggedUser.EmpresaId,
                    PontoRetiradaId = model.PontoRetiradaId,
                    DataPedido = model.DataPedido.Value,
                    NotaFiscal = model.NotaFiscal,
                    NumeroPedido = model.NumeroPedido
                });

                return Ok(Url.RouteUrl("Encomenda_PontoVenda_Index"));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_CadastroEncomenda", model);
            }
        }
        #endregion

        #region ::: PONTO DE RETIRADA :::
        [Authorize(Roles = Roles.PONTO_RETIRADA)]
        [Route("ponto-retirada/minhas-encomendas", Name = "Encomenda_PontoRetirada_Inicio")]
        public async Task<IActionResult> MinhaEncomendas()
        {
            IList<MinhasEncomendasViewModel> model = new List<MinhasEncomendasViewModel>();
            var encomendas = await _encomendaService.ObterPorPontoDeRetirada(_loggedUser.EmpresaId);

            foreach (var item in encomendas)
            {
                string nomePontoVenda = await _empresaService.ObterNomeEmpresa(item.PontoVendaId);

                model.Add(new MinhasEncomendasViewModel
                {
                    Id = item.Id,
                    DataPedido = item.DataPedido,
                    NumeroPedido = item.NumeroPedido,
                    TipoMovimento = item.ObterEstadoAtual().GetDescription(),
                    PontoVendaNome = nomePontoVenda,
                    PontoVendaId = item.PontoVendaId,
                    UrlEncomendaDetalhe = Url.RouteUrl("Encomenda_PontoRetirada_Detalhe", new
                    {
                        encomendaId = item.Id
                    })
                });
            }

            return PartialView("PontoRetiradaIndex", model);
        }

        [Authorize(Roles = Roles.PONTO_RETIRADA)]
        [Route("ponto-retirada/minhas-encomendas/{encomendaId}/detalhe", Name = "Encomenda_PontoRetirada_Detalhe")]
        public async Task<IActionResult> MinhasEncomendasDetalhe(Guid encomendaId)
        {
            var encomenda = await _encomendaService.ObterPorId(encomendaId);
            string nomePontoVenda = await _empresaService.ObterNomeEmpresa(encomenda.PontoVendaId);

            var model = new EncomendaDetalheViewModel
            {
                Id = encomenda.Id,
                DataPedido = encomenda.DataPedido,
                NotaFiscal = encomenda.NotaFiscal ?? " - ",
                NumeroPedido = encomenda.NumeroPedido,
                PontoVendaNome = nomePontoVenda,
                PontoVendaId = encomenda.PontoVendaId,
                PermiteMovimentar = encomenda.PontoVendaPodeMovimentar(),
                Movimentacao = encomenda.GetHistoria()?
                                        .Select(x => new EncomendaHistoriaViewModel
                                        {
                                            EncomendaId = x.EncomendaId,
                                            Nome = x.TipoMovimento.GetDescription(),
                                            DataMovimento = x.DataCadastro
                                        }),
                MovimentosPermitidos = encomenda
                                        .ObterMovimentosPermitidos(TipoEmpresa.PontoRetirada)
                                        .Select(x => new SelectListItem(text: x.GetDescription(), value: x.ToString()))
            };

            return View("PontoRetiradaDetalhe", model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.PONTO_RETIRADA)]
        [ValidateAntiForgeryToken]
        [Route("ponto-retirada/minhas-encomendas/{encomendaId}/movimentar")]
        public async Task<IActionResult> MovimentarEncomenda([FromRoute] Guid encomendaId, string movimento)
        {
            try
            {
                if (!Enum.TryParse(movimento, out TipoMovimento tipoMovimento))
                    return BadRequest("Invalid request");

                await _encomendaService.AdicionarMovimento(encomendaId, _loggedUser.EmpresaId, tipoMovimento);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
