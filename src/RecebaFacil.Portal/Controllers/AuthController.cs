using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models.Auth;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService authService,
                              IHttpContextService httpContextService)
            : base(httpContextService)
        {
            _AuthService = authService;
        }

        [HttpGet("entrar", Name = "Auth_Entrar")]
        public IActionResult Index()
        {
            return View();

        }

        [HttpPost("autenticar", Name = "Auth_Autenticar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LogonViewModel model)
        {
#if DEBUG
            model = new LogonViewModel() { LoginName = "josefaemarina", Senha = "123qwe" };
#endif
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                Guid usuarioId = await _AuthService.Autenticar(model.LoginName, model.Senha);

                await _httpContextService.SignIn(usuarioId);

                return RedirectToAction("Hub", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("sair", Name = "Auth_Sair")]
        public IActionResult Sair()
        {
            _httpContextService.SignOut();

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
