using Microsoft.Extensions.Logging;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace RecebaFacil.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly ILogger<IUsuarioService> _logger;
        private readonly ISecurityService _securityService;
        private readonly IGrupoService _grupoService;
        private readonly IEmpresaService _empresaService;

        public UsuarioService(IRepositoryUsuario repositoryUSuario,
                              ILogger<IUsuarioService> logger,
                              ISecurityService securityService,
                              IGrupoService grupoService, 
                              IEmpresaService empresaService)
        {
            _repositoryUsuario = repositoryUSuario;
            _logger = logger;
            _securityService = securityService;
            _grupoService = grupoService;
            _empresaService = empresaService;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            try
            {
                var usuario = await _repositoryUsuario.ObterPorId(id);
                usuario.Grupo = await _grupoService.ObterPorId(usuario.GrupoId);
                usuario.Empresa = await _empresaService.ObterPorId(usuario.EmpresaId);

                return usuario;
            }
            catch (Exception ex)
            {
                _logger.LogError("UsuarioService.ObterPorId", ex.Message);
                throw new RecebaFacilException("Usuário não encontrado");
            }
        }

        public async Task NovoUsuario(Usuario usuario)
        {
            usuario.Id = Guid.NewGuid();
            usuario.Bloqueado = false;
            usuario.TrocarSenha = false;

            try
            {
                usuario.Senha = _securityService.HashValue(usuario.Senha);

                await _repositoryUsuario.Salvar(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError("UsuarioService.NovoUsuario", ex.Message);
                throw new RecebaFacilException("Erro ao cadastrar novo usuário");
            }
        }
    }
}
