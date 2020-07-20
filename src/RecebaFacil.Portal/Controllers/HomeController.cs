using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Models.Home;
using RecebaFacil.Portal.Services.Interfaces;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPreRegistroService _PreRegistroService;
        public HomeController(IHttpContextService httpContextService,
                              IPreRegistroService preRegistroService)
            : base(httpContextService)
        {
            _PreRegistroService = preRegistroService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost("cadastrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(CadastrarViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return PartialView("Cadastrar",model);

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

                return PartialView("_ModalCadastro");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
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
        public IActionResult Hub() => RedirectToRoute(_httpContextService.ObterRotaInicial());
        [Authorize]
        [HttpGet("receba-facil/suporte", Name = "Home_Suporte")]
        public IActionResult Suporte()
        {
            return View();
        }
        #endregion
    }
}
