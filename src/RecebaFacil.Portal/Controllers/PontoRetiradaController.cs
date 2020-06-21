using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models;
using RecebaFacil.Portal.Models.PontoRetirada;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize(Roles = Roles.PONTO_RETIRADA)]
    [Route("ponto-retirada")]
    public class PontoRetiradaController : BaseWithCacheController
    {
        private readonly IEmpresaService _empresaService;
        private readonly IEnderedecoService _enderedecoService;
        private readonly IExpedienteService _expedienteService;

        public PontoRetiradaController(IHttpContextService httpContextService,
                                       ICacheService cacheService,
                                       IEnderedecoService enderedecoService,
                                       IEmpresaService empresaService,
                                       IExpedienteService expedienteService)
            : base(httpContextService, cacheService)
        {
            _enderedecoService = enderedecoService;
            _empresaService = empresaService;
            _expedienteService = expedienteService;
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
                RouteEndereco = Url.RouteUrl("PontoRetirada_MeuPerfil_Endereco", new { enderecoId = _loggedUser.EmpresaId }),
                RouteExpediente = Url.RouteUrl("PontoRetirada_MeuPerfil_Expediente", new { empresaId = _loggedUser.EmpresaId })
            };

            return View(model);
        }

        [Route("meu-perfil/usuario", Name = "PontoRetirada_Perfil")]
        public async Task<IActionResult> Perfil()
        {
            var empresa = await _empresaService.ObterPorId(_loggedUser.EmpresaId);
            PerfilViewModel model = new PerfilViewModel
            {
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Cnpj = empresa.Cnpj,
                DataCadastro = empresa.DataCadastro,
                Usuario = _loggedUser.Login
            };

            return PartialView("_Perfil", model);
        }

        [Route("meu-perfil/endereco/{enderecoId}", Name = "PontoRetirada_MeuPerfil_Endereco")]
        public async Task<IActionResult> MeuEndereco(string enderecoId)
        {
            var endereco = await _enderedecoService.ObterAtivoPorEmpresa(Guid.Parse(enderecoId));

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
        public async Task<IActionResult> MeuExpediente(string empresaId)
        {
            var expediente = await _expedienteService.ObterPorEmpresa(Guid.Parse(empresaId));

            var model = expediente.Select(x => new ExpedienteViewModel
            {
                Id = x.Id,
                DiaSemana =  x.DiaSemana,
                HoraAbertura = x.HoraAbertura,
                HoraEncerramento = x.HoraEncerramento
            }).OrderBy(x => x.DiaSemana);

            return PartialView("_Expediente", model);
        }
    }
}
