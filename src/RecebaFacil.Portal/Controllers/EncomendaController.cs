using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Entities;
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
                    TipoMovimento = item.Historia.Max(x => x.TipoMovimento).GetDescription(),
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
                    NotaFiscal = item.NotaFiscal,
                    NumeroPedido = item.NumeroPedido,
                    TipoMovimento = item.Historia.Max(x => x.TipoMovimento).GetDescription(),
                    PontoVendaNome = nomePontoVenda,
                    PontoVendaId = item.PontoVendaId,

                });
            }

            return PartialView("PontoRetiradaIndex", model);
        }

    }
}
