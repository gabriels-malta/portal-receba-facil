using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models.Auth;
using RecebaFacil.Portal.Services.Interfaces;
using System;

namespace RecebaFacil.Portal.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _AuthService;
        private readonly IPreRegistroService _PreRegistroService;

        public AuthController(IAuthService authService,
                              IPreRegistroService preRegistroService,
                              IHttpContextService httpContextService)
            : base(httpContextService)
        {
            _AuthService = authService;
            _PreRegistroService = preRegistroService;
        }

        [HttpGet("", Name = "Auth_Entrar")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("autenticar", Name = "Auth_Autenticar")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LogonViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                Guid usuarioId = _AuthService.Autenticar(model.LoginName, model.Senha);

                _httpContextService.SignIn(usuarioId);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        [HttpGet("pre-registro", Name = "Auth_PreRegistro")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("registrar", Name = "Auth_Registrar")]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                _PreRegistroService.Salvar(new PreRegistro
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

                return PartialView("_PreRegisterModal");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost("sair", Name = "Auth_Sair")]
        public IActionResult Sair()
        {
            _httpContextService.SignOut();

            return RedirectToRoute("Auth_Entrar");
        }
    }
}
