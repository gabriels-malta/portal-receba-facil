using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecebaFacil.Portal.Services.Interfaces;

namespace RecebaFacil.Portal.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IHttpContextService _httpContextService;

        public BaseController(IHttpContextService httpContextService)
        {
            _httpContextService = httpContextService;
        }
    }
}
