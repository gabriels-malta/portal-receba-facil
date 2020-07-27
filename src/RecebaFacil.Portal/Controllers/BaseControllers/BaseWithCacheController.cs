using Microsoft.AspNetCore.Mvc.Filters;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Core.Model;
using RecebaFacil.Portal.Services.Interfaces;

namespace RecebaFacil.Portal.Controllers
{
    public class BaseWithCacheController : BaseController
    {
        protected readonly ICacheService _cacheService;
        protected readonly LoggedUser _loggedUser;

        public BaseWithCacheController(IHttpContextService httpContextService,
                                       ICacheService cacheService)
            : base(httpContextService)
        {
            _cacheService = cacheService;
            _loggedUser = _cacheService.Obter<LoggedUser>(CacheKeys.UsuarioLogado);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            TempData["UsuarioLogin"] = _loggedUser.Login;
            TempData["NomeEmpresa"] = _loggedUser.Empresa;

            base.OnActionExecuted(context);
        }
    }
}
