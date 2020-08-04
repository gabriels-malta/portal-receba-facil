using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Core.Model;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Helpers;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Models.Home;
using RecebaFacil.Portal.Services;
using RecebaFacil.Portal.Services.Interfaces;
using RecebaFacil.Portal.Services.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SelectListItemHelper _selectListItemHelper;
        private readonly IPreRegistroService _PreRegistroService;
        private readonly ICacheService _cacheService;
        private readonly IIBGEMicrorregioesService _microrregioesService;
        public HomeController(IHttpContextService httpContextService,
                              IPreRegistroService preRegistroService,
                              ICacheService cacheService,
                              SelectListItemHelper selectListItemHelper,
                              IIBGEMicrorregioesService microrregioesService)
            : base(httpContextService)
        {
            _PreRegistroService = preRegistroService;
            _cacheService = cacheService;
            _selectListItemHelper = selectListItemHelper;
            _microrregioesService = microrregioesService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (_cacheService.TentarObter(CacheKeys.UsuarioLogado, out LoggedUser _))
                return RedirectToAction("Hub");


            // redireciona para página institucional
            return View();
        }

        [HttpGet("cadastrar")]
        public async Task<IActionResult> Cadastrar()
        {
            CadastrarViewModel model = new CadastrarViewModel();

            if (!_cacheService.TentarObter(CacheKeys.Microrregioes, out IEnumerable<Microrregioes> microrregioes))
            {
                microrregioes = await _microrregioesService.ObterTodos();
                _ = _cacheService.CriarOuObter(CacheKeys.Microrregioes, microrregioes, TimeSpan.FromDays(90));
            }

            model.Municipios = _selectListItemHelper.MontarListaDeMunicipios(microrregioes);

            return View(model);
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(CadastrarViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Municipios = _selectListItemHelper.MontarListaDeMunicipios(_cacheService.Obter<IEnumerable<Microrregioes>>(CacheKeys.Microrregioes));
                    return View(model);
                }

                await _PreRegistroService.Salvar(new PreRegistro
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Telefone = model.Telefone,
                    Cidade = model.Cidade,
                    Endereco = model.Endereco,
                    Cnpj = model.Cnpj,
                    NomeEmpresa = model.NomeEmpresa,
                    Objetivo = model.Objetivo
                });

                TempData.Add("success", true);

                return View(new CadastrarViewModel());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.Municipios = _selectListItemHelper.MontarListaDeMunicipios(_cacheService.Obter<IEnumerable<Microrregioes>>(CacheKeys.Microrregioes));
                return View(model);
            }

        }




        #region Authorized Only
        [Authorize]
        [Route("Error", Name = "Home_Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpGet("receba-facil/suporte", Name = "Home_Suporte")]
        public async Task<IActionResult> Suporte([FromQuery] string form)
        {
            SuporteViewModel model = new SuporteViewModel();
            if ("ad8bace88e1844debdcae07839f4e2fd" == form)
            {
                if (!_cacheService.TentarObter(CacheKeys.Microrregioes, out IEnumerable<Microrregioes> microrregioes))
                {
                    microrregioes = await _microrregioesService.ObterTodos();
                    _ = _cacheService.CriarOuObter(CacheKeys.Microrregioes, microrregioes, TimeSpan.FromDays(90));
                }

                if (_cacheService.TentarObter(CacheKeys.UsuarioLogado, out LoggedUser loggedUser))
                {
                    model.PartialViewName = "_SuporteNovoEndereco";
                    model.NovoEnderecoViewModel = new SuporteNovoEnderecoViewModel
                    {
                        EmpresaId = loggedUser.EmpresaId,
                        EmpresaNome = loggedUser.Empresa,
                        Municipios = _selectListItemHelper.MontarListaDeMunicipios(microrregioes)
                    };
                }
                else
                    model.PartialViewName = null;
            }

            return View(model);
        }

        [Authorize]
        [HttpGet("pagina-inicial")]
        public IActionResult Hub() => RedirectToRoute(_httpContextService.ObterRotaInicial());
        #endregion
    }
}
