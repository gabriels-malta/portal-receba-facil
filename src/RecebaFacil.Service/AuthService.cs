using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;

namespace RecebaFacil.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _UsuarioService;
        private readonly ISecurityService _SecurityService;

        public AuthService(IUsuarioService usuarioService,
                           ISecurityService securityService)
        {
            _UsuarioService = usuarioService;
            _SecurityService = securityService;
        }

        public int Autenticar(string email, string senha)
        {
            string _senha = _SecurityService.HashValue(senha);
            Usuario usuario = _UsuarioService.BuscarPorAutenticacao(email, _senha);

            if (usuario.Bloqueado)
                throw new RecebaFacilException("Usuário está bloqueado. Entre em contato co o administrador do sistema");

            if(usuario.TrocarSenha)
                throw new RecebaFacilException("Por favor, altere sua senha");

            return usuario.Id;
        }
    }
}
