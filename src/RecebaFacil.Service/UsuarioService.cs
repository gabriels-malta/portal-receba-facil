﻿using Microsoft.Extensions.Logging;
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

        public UsuarioService(IRepositoryUsuario repositoryUSuario,
                              ILogger<IUsuarioService> logger,
                              ISecurityService securityService)
        {
            _repositoryUsuario = repositoryUSuario;
            _logger = logger;
            _securityService = securityService;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            try
            {
                var usuario = await _repositoryUsuario.ObterPorId(id);

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
