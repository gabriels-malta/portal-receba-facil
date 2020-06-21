using Moq;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using RecebaFacil.Service;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RecebaFacil.Tests
{
    public class AuthServiceTest
    {
        private IAuthService _authService;

        private Mock<IRepositoryUsuario> mockRepositoryUsuario = new Mock<IRepositoryUsuario>();
        private Mock<ISecurityService> mockSecurityService = new Mock<ISecurityService>();

        public AuthServiceTest()
        {
            mockSecurityService.Setup(x => x.HashValue("123qwe")).Returns("qwe123");
        }

        [Fact]
        public void Deve_Retornar_Usuario_Bloqueado()
        {
            mockRepositoryUsuario
                .Setup(x => x.ObterPrimeiroPor(u => u.Login == It.IsAny<string>() && u.Senha == It.IsAny<string>(), default))
                .Returns(Task.FromResult(new Usuario { Bloqueado = true }));

            _authService = new AuthService(mockRepositoryUsuario.Object, mockSecurityService.Object);

            RecebaFacilException ex = Assert.Throws<RecebaFacilException>(() =>
            {
                _authService.Autenticar("login", "123qwe");
            });

            Assert.Equal("Usuário está bloqueado. Entre em contato co o administrador do sistema", ex.Message);
        }

        [Fact]
        public void Deve_Retornar_Usuario_Com_Senha_Bloqueada()
        {
            mockRepositoryUsuario
                .Setup(x => x.ObterPrimeiroPor(u => u.Login == "login" && u.Senha == "qwe123", default).Result)
                .Returns(new Usuario { TrocarSenha = true });

            _authService = new AuthService(mockRepositoryUsuario.Object, mockSecurityService.Object);

            RecebaFacilException ex = Assert.Throws<RecebaFacilException>(() =>
            {
                _authService.Autenticar("login", "123qwe");
            });

            Assert.Equal("Por favor, altere sua senha", ex.Message);
        }

        [Fact]
        public void Deve_Retornar_Um_Usuario()
        {
            mockRepositoryUsuario
                .Setup(x => x.ObterPrimeiroPor(u => u.Login == "login" && u.Senha == "qwe123", default).Result)
                .Returns(new Usuario { Id = Guid.Parse("8053841b-325c-4631-bf13-729df865d911") });

            _authService = new AuthService(mockRepositoryUsuario.Object, mockSecurityService.Object);

            Guid usuarioId = _authService.Autenticar("login", "123qwe");

            Assert.Equal(Guid.Parse("8053841b-325c-4631-bf13-729df865d911"), usuarioId);
        }
    }
}
