using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models.PontoRetirada;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecebaFacil.Portal.Controllers
{
    [Authorize(Roles = AuthorizedRoles.Get)]
    [Route("meu-perfil")]
    public class PerfilController : BaseWithCacheController
    {
        private readonly IEmpresaService _empresaService;
        private readonly IEnderedecoService _enderedecoService;
        private readonly IExpedienteService _expedienteService;
        public PerfilController(IHttpContextService httpContextService,
                                ICacheService cacheService,
                                IEmpresaService empresaService,
                                IEnderedecoService enderedecoService,
                                IExpedienteService expedienteService)
            : base(httpContextService, cacheService)
        {
            _empresaService = empresaService;
            _enderedecoService = enderedecoService;
            _expedienteService = expedienteService;
        }

        [HttpGet(Name = "MeuPerfil")]
        public IActionResult MeuPerfil()
        {
            MeuPerfilViewModel model = new MeuPerfilViewModel
            {
                RoutePerfil = Url.RouteUrl("Perfil"),
                RouteEndereco = Url.RouteUrl("MeuPerfil_Endereco", new { enderecoId = _loggedUser.EmpresaId }),
                RouteExpediente = Url.RouteUrl("MeuPerfil_Expediente", new { empresaId = _loggedUser.EmpresaId }),
                ExibirAbaExpediente = _loggedUser.Role == Roles.PONTO_RETIRADA
            };

            return View(model);
        }

        [Route("usuario", Name = "Perfil")]
        public async Task<IActionResult> Perfil()
        {
            var empresa = await _empresaService.ObterPorId(_loggedUser.EmpresaId);

            PerfilViewModel model = new PerfilViewModel
            {
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Cnpj = empresa.Cnpj,
                DataCadastro = empresa.DataCadastro,
                Usuario = _loggedUser.Login,
                Contatos = empresa.Contatos.Select(c => new ContatoViewModel
                {
                    Id = c.Id,
                    Valor = c.Valor
                })
            };

            return PartialView("_Perfil", model);
        }

        [Route("{enderecoId}/endereco", Name = "MeuPerfil_Endereco")]
        public async Task<IActionResult> MeuEndereco(Guid enderecoId)
        {
            var endereco = await _enderedecoService.ObterAtivoPorEmpresa(enderecoId);

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

        [Route("{empresaId}/expediente", Name = "MeuPerfil_Expediente")]
        public async Task<IActionResult> MeuExpediente(Guid empresaId)
        {
            var expediente = await _expedienteService.ObterPorEmpresa(empresaId);

            var model = expediente
                .OrderBy(x => (int)x.DiaSemana)
                .Select(x => new ExpedienteViewModel
                {
                    Id = x.Id,
                    DiaSemana = x.DiaSemana.ToString(),
                    HoraAbertura = x.HoraAbertura,
                    HoraEncerramento = x.HoraEncerramento
                });

            return PartialView("_Expediente", model);
        }
    }
}
