using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Models.Home;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPreRegistroService _PreRegistroService;
        private readonly IMemoryCache _cache;
        public HomeController(IHttpContextService httpContextService,
                              IPreRegistroService preRegistroService,
                              IMemoryCache cache)
            : base(httpContextService)
        {
            _PreRegistroService = preRegistroService;
            _cache = cache;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("cadastrar")]
        public async Task<IActionResult> Cadastrar()
        {
            CadastrarViewModel model = new CadastrarViewModel();

            if (!_cache.TryGetValue(CacheKeys.Microrregioes, out IEnumerable<IBGEMicrorregioes> microrregioes))
            {
                using HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados/sp/municipios");
                microrregioes = JsonConvert.DeserializeObject<IEnumerable<IBGEMicrorregioes>>(await response.Content.ReadAsStringAsync());
                _ = _cache.Set(CacheKeys.Microrregioes, microrregioes, TimeSpan.FromDays(90));
            }

            model.MontarListaDeMunicipios(microrregioes);

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
                    model.MontarListaDeMunicipios(_cache.Get<IEnumerable<IBGEMicrorregioes>>(CacheKeys.Microrregioes));
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
                model.MontarListaDeMunicipios(_cache.Get<IEnumerable<IBGEMicrorregioes>>(CacheKeys.Microrregioes));
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
        public IActionResult Suporte()
        {
            return View();
        }

        [Authorize]
        public IActionResult Hub() => RedirectToRoute(_httpContextService.ObterRotaInicial());
        #endregion
    }
}
