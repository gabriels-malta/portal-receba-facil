using Moq;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Service;
using Xunit;

namespace RecebaFacil.Tests
{
    public class AuthServiceTest
    {
        private IAuthService _authService;
        private IUsuarioService _usuarioService;
        private ISecurityService _securityService;
        private Mock<IUsuarioService> mockUsuarioService = new Mock<IUsuarioService>();
        private Mock<ISecurityService> mockSecurityService = new Mock<ISecurityService>();

        public AuthServiceTest()
        {
            mockSecurityService.Setup(x => x.HashValue("123qwe")).Returns("qwe123");
        }

        [Fact]
        public void Deve_Retornar_Usuario_Bloqueado()
        {
            mockUsuarioService
                .Setup(x => x.BuscarPorAutenticacao("login", "qwe123"))
                .Returns(new Usuario { Bloqueado = true });
            _usuarioService = mockUsuarioService.Object;
            _securityService = mockSecurityService.Object;

            _authService = new AuthService(_usuarioService, _securityService);

            RecebaFacilException ex = Assert.Throws<RecebaFacilException>(() =>
            {
                _authService.Autenticar("login", "123qwe");
            });

            Assert.Equal("Usuário está bloqueado. Entre em contato co o administrador do sistema", ex.Message);
        }

        [Fact]
        public void Deve_Retornar_Usuario_Com_Senha_Bloqueada()
        {
            mockUsuarioService
                .Setup(x => x.BuscarPorAutenticacao("login", "qwe123"))
                .Returns(new Usuario { TrocarSenha = true });
            _usuarioService = mockUsuarioService.Object;
            _securityService = mockSecurityService.Object;

            _authService = new AuthService(_usuarioService, _securityService);

            RecebaFacilException ex = Assert.Throws<RecebaFacilException>(() =>
            {
                _authService.Autenticar("login", "123qwe");
            });

            Assert.Equal("Por favor, altere sua senha", ex.Message);
        }

        [Fact]
        public void Deve_Retornar_Um_Usuario()
        {
            mockUsuarioService
                .Setup(x => x.BuscarPorAutenticacao("login", "qwe123"))
                .Returns(new Usuario { Id = 789 });
            _usuarioService = mockUsuarioService.Object;
            _securityService = mockSecurityService.Object;

            _authService = new AuthService(_usuarioService, _securityService);
            
            int usuarioId = _authService.Autenticar("login", "123qwe");

            Assert.Equal(789, usuarioId);
        }
    }
}
