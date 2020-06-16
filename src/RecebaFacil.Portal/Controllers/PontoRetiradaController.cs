using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain.Application.Constants;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Extensions;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Models.PontoRetirada;
using RecebaFacil.Portal.Services.Interfaces;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize(Roles = Roles.PONTO_RETIRADA)]
    [Route("ponto-retirada")]
    public class PontoRetiradaController : BaseWithCacheController
    {
        private readonly IEmpresaService _empresaService;
        private readonly IEnderedecoService _enderedecoService;

        public PontoRetiradaController(IHttpContextService httpContextService,
                                       ICacheService cacheService,
                                       IEnderedecoService enderedecoService, 
                                       IEmpresaService empresaService)
            : base(httpContextService, cacheService)
        {
            _enderedecoService = enderedecoService;
            _empresaService = empresaService;
        }

        [Route("inicio", Name = "PontoRetirada_Inicio")]
        public IActionResult Index()
        {
            InicioViewModel model = new InicioViewModel
            {
                Header = HeaderViewModel.Create(_loggedUser)
            };

            model.Header.AdicionarRotaMeuPerfil("PontoRetirada_MeuPerfil");

            return View(model);
        }

        [Route("meu-perfil", Name = "PontoRetirada_MeuPerfil")]
        public IActionResult MeuPerfil()
        {
            MeuPerfilViewModel model = new MeuPerfilViewModel
            {
                Header = HeaderViewModel.Create(_loggedUser),
                RoutePerfil = Url.RouteUrl("PontoRetirada_Perfil"),
                RouteEndereco = Url.RouteUrl("PontoRetirada_MeuPerfil_Endereco", new { enderecoId = _loggedUser.EmpresaId.ToHex() }),
                RouteExpediente = Url.RouteUrl("PontoRetirada_MeuPerfil_Expediente", new { empresaId = _loggedUser.EmpresaId.ToHex() })
            };

            return View(model);
        }

        [Route("meu-perfil/usuario", Name = "PontoRetirada_Perfil")]
        public IActionResult Perfil()
        {
            var empresa = _empresaService.ObterPorId(_loggedUser.EmpresaId);
            PerfilViewModel model = new PerfilViewModel
            {
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Cnpj = empresa.Cnpj,
                DataCadastro = empresa.DataCadastro,
                Contato = _loggedUser.Contato,
                Usuario = _loggedUser.Login
            };

            return PartialView("_Perfil", model);
        }

        [Route("meu-perfil/endereco/{enderecoId}", Name = "PontoRetirada_MeuPerfil_Endereco")]
        public IActionResult MeuEndereco(string enderecoId)
        {
            var endereco = _enderedecoService.ObterPorEmpresa(enderecoId.FromHex());

            var model = new MeuEnderecoViewModel()
            {
                EnderecoId = endereco.Id,
                Cep = endereco.Cep,
                Bairro = endereco.Bairro,
                Logradouro = endereco.Logradouro,
                Municipio = endereco.Municipio,
                Uf = endereco.Uf,
                Observacao = endereco.Observacao,
            };
            return PartialView("_Endereco", model);
        }

        [Route("meu-perfil/expediente/{empresaId}", Name = "PontoRetirada_MeuPerfil_Expediente")]
        public IActionResult MeuExpediente(string empresaId)
        {
            return PartialView("_Expediente", empresaId.FromHex());
        }
    }
}
