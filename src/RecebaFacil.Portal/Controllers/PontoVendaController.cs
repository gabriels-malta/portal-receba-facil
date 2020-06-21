using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Services.Interfaces;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize(Roles = Roles.PONTO_VENDA)]
    [Route("ponto-venda")]
    public class PontoVendaController : BaseWithCacheController
    {
        public PontoVendaController(IHttpContextService httpContextService,
                                    ICacheService cacheService)
            : base(httpContextService, cacheService)
        {
        }

        [Route("inicio", Name = "PontoVenda_Inicio")]
        public IActionResult Index()
        {
            InicioViewModel model = new InicioViewModel
            {
                Header = HeaderViewModel.Create(_loggedUser)
            };

            return View(model);
        }
    }
}
