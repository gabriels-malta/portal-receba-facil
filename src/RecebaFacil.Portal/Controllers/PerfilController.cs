using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecebaFacil.Domain;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Services;
using RecebaFacil.Portal.Models.PontoRetirada;
using RecebaFacil.Portal.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using static RecebaFacil.Portal.Models.PontoRetirada.MeuEnderecoViewModel;

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
                TipoEmpresa = empresa.TipoEmpresa.GetDescription(),
                RazaoSocial = empresa.RazaoSocial,
                NomeFantasia = empresa.NomeFantasia,
                Cnpj = empresa.Cnpj,
                DataCadastro = empresa.DataCadastro,
                Usuario = _loggedUser.Login,
                Contatos = empresa.Contatos.Select(c => new ContatoViewModel
                {
                    Id = c.Id,
                    Valor = c.Valor,
                    TipoContato = c.TipoContato.GetDescription()
                })
            };

            return PartialView("_Perfil", model);
        }

        [Route("{enderecoId}/endereco", Name = "MeuPerfil_Endereco")]
        public async Task<IActionResult> MeuEndereco(Guid enderecoId)
        {
            MeuEnderecoViewModel model = new MeuEnderecoViewModel();

            var enderecos = await _enderedecoService.ObterPorEmpresa(enderecoId);
            model.Configure(enderecos.Select(endereco => new EnderecoViewModel()
            {
                EnderecoId = endereco.Id,
                Cep = endereco.Cep,
                Bairro = endereco.Bairro,
                Logradouro = endereco.Logradouro,
                Municipio = endereco.Municipio,
                Uf = endereco.Uf,
                Observacao = endereco.Observacao,
                Ativo = endereco.Ativo
            }));

            return PartialView("_Endereco", model);
        }

        [Authorize(Roles = Roles.PONTO_RETIRADA)]
        [Route("{empresaId}/expediente", Name = "MeuPerfil_Expediente")]
        public async Task<IActionResult> MeuExpediente(Guid empresaId)
        {
            var expediente = await _expedienteService.ObterPorEmpresa(empresaId);

            var model = expediente
                .OrderBy(x => (int)x.DiaSemana)
                .Select(x => new ExpedienteViewModel
                {
                    Id = x.Id,
                    DiaSemana = x.DiaSemana.GetDescription(),
                    HoraAbertura = x.HoraAbertura.ToString(@"hh\:mm"),
                    HoraEncerramento = x.HoraEncerramento.ToString(@"hh\:mm")
                });

            return PartialView("_Expediente", model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.PONTO_RETIRADA)]
        [Route("expediente/novo", Name = "MeuPerfil_Expediente_Gravar")]
        public async Task<IActionResult> AtualizarExpediente(ExpedienteViewModel model)
        {
            try
            {
                await _expedienteService.Salvar(new Domain.Entities.Expediente
                {
                    PontoRetiradaId = _loggedUser.EmpresaId,
                    DiaSemana = Enum.Parse<DiaSemana>(model.DiaSemana),
                    HoraAbertura = TimeSpan.Parse(model.HoraAbertura),
                    HoraEncerramento = TimeSpan.Parse(model.HoraEncerramento),
                    Id = model.Id
                });

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
