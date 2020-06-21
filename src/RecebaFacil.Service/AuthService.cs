using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;

namespace RecebaFacil.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly ISecurityService _SecurityService;

        public AuthService(IRepositoryUsuario repositoryUsuario,
                           ISecurityService securityService)
        {
            _repositoryUsuario = repositoryUsuario;
            _SecurityService = securityService;
        }

        public Guid Autenticar(string email, string senha)
        {
            string _senha = _SecurityService.HashValue(senha);

            Usuario usuario = _repositoryUsuario.ObterPrimeiroPor(x => email == x.Login && _senha == x.Senha)
                .GetAwaiter()
                .GetResult();

            if (usuario.Bloqueado)
                throw new RecebaFacilException("Usuário está bloqueado. Entre em contato co o administrador do sistema");

            if (usuario.TrocarSenha)
                throw new RecebaFacilException("Por favor, altere sua senha");

            return usuario.Id;
        }
    }
}
