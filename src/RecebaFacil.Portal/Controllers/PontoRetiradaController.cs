using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain;
using RecebaFacil.Portal.Services.Interfaces;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize(Roles = Roles.PONTO_RETIRADA)]
    [Route("ponto-retirada")]
    public class PontoRetiradaController : BaseWithCacheController
    {
        public PontoRetiradaController(IHttpContextService httpContextService,
                                       ICacheService cacheService)
            : base(httpContextService, cacheService)
        {
        }

        [Route("inicio", Name = "PontoRetirada_Inicio")]
        public IActionResult Index()
        {
            ViewBag.EncomendasUrl = Url.RouteUrl("Encomenda_PontoRetirada_Inicio");
            return View();
        }
    }
}
