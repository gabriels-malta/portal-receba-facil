using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading.Tasks;

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

        public async Task<Guid> Autenticar(string email, string senha)
        {
            string _senha = _SecurityService.HashValue(senha);

            Usuario usuario = await _repositoryUsuario.ObterPrimeiroPor(x => email == x.Login && _senha == x.Senha);

            if (usuario == null)
                throw new RecebaFacilException("Usuário ou senha inválido");

            if (usuario.Bloqueado)
                throw new RecebaFacilException("Usuário está bloqueado. Entre em contato com o administrador do sistema");

            if (usuario.TrocarSenha)
                throw new RecebaFacilException("Por favor, altere sua senha");

            return usuario.Id;
        }
    }
}
