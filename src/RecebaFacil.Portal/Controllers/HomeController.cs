using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Services.Interfaces;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(IHttpContextService httpContextService)
            : base(httpContextService)
        { }

        public IActionResult Index()
        {
            string rotaInicial = _httpContextService.ObterRotaInicial();

            return RedirectToRoute(rotaInicial);
        }

        [HttpGet("receba-facil/suporte", Name = "Home_Suporte")]
        public IActionResult Suporte()
        {
            return View();
        }

        [Route("Error", Name = "Home_Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = HttpContext.TraceIdentifier });
        }
    }
}
