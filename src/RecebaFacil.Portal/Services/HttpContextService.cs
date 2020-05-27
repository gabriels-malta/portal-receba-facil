﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using RecebaFacil.Domain.Application.Constants;
using RecebaFacil.Domain.Application.Model;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurityService _securityService;
        private readonly ICacheService _cacheService;

        public HttpContextService(IHttpContextAccessor httpContextAccessos,
                                  IUsuarioService usuarioService,
                                  ISecurityService securityService,
                                  ICacheService cacheService)
        {
            _httpContextAccessor = httpContextAccessos;
            _usuarioService = usuarioService;
            _securityService = securityService;
            _cacheService = cacheService;
        }

        public async Task SignIn(int usuarioId)
        {
            Usuario usuario = _usuarioService.BuscarPorId(usuarioId);
            string encryptedUsuarioId = _securityService.EncryptValue(usuario.Id);

            IEnumerable<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.Role, usuario.Grupo.Role),
                new Claim(ClaimTypes.NameIdentifier, encryptedUsuarioId)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal();
            principal.AddIdentity(claimsIdentity);

            CriarCacheUsuario(usuario);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal).ConfigureAwait(false);
        }

        public async Task SignOut()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync().ConfigureAwait(false);
        }

        public string ObterRole => GetRoleValue(ClaimTypes.Role);

        public int ObterIdUsuarioLogado()
        {
            string usuarioCript = GetRoleValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(_securityService.DecryptValue(usuarioCript), out int usuarioId))
            {
                return usuarioId;
            }
            return -1;
        }

        public string ObterRotaInicial()
        {
            switch (ObterRole)
            {
                case Roles.PONTO_RETIRADA:
                    return "PontoRetirada_Inicio";
                case Roles.PONTO_VENDA:
                    return "PontoVenda_Inicio";
                default:
                    _httpContextAccessor.HttpContext.TraceIdentifier = "Erro ao identificar perfil do usuário";
                    return "Home_Error";
            }
        }

        private string GetRoleValue(string type)
        {
            Claim role = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == type);

            return role?.Value;
        }

        private void CriarCacheUsuario(Usuario usuario)
        {
            LoggedUser usuarioLogado = new LoggedUser(usuario.Login,
                                                      usuario.Grupo.Role,
                                                      usuario.Contato?.Empresa.NomeFantasia,
                                                      usuario.Contato?.Valor,
                                                      usuario.Id,
                                                      usuario.Contato.EmpresaID,
                                                      usuario.GrupoId,
                                                      usuario.ContatoId.GetValueOrDefault(-1));

            _cacheService.Limpar(CacheKeys.UsuarioLogado);
            _cacheService.CriarOuObter(CacheKeys.UsuarioLogado, usuarioLogado);
        }
    }
}
