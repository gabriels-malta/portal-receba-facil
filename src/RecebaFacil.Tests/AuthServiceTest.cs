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
                .ReturnsAsync(new Usuario { Bloqueado = true });

            _authService = new AuthService(mockRepositoryUsuario.Object, mockSecurityService.Object);

            Task.Run(async () =>
            {
                RecebaFacilException recebaFacilException = await Assert.ThrowsAsync<RecebaFacilException>(async () =>
                        {
                            await _authService.Autenticar("login", "123qwe");
                        });

                Assert.Equal("Usuário está bloqueado. Entre em contato com o administrador do sistema", recebaFacilException.Message);
            });
        }

        [Fact]
        public void Deve_Retornar_Usuario_Com_Senha_Bloqueada()
        {
            mockRepositoryUsuario
                .Setup(x => x.ObterPrimeiroPor(u => u.Login == It.IsAny<string>() && u.Senha == It.IsAny<string>(), default))
                .ReturnsAsync(new Usuario { TrocarSenha = true });

            _authService = new AuthService(mockRepositoryUsuario.Object, mockSecurityService.Object);

            Task.Run(async () =>
            {
                RecebaFacilException recebaFacilException = await Assert.ThrowsAsync<RecebaFacilException>(async () =>
                    {
                        await _authService.Autenticar("login", "123qwe");
                    });

                Assert.Equal("Por favor, altere sua senha", recebaFacilException.Message);
            });
        }

        [Fact]
        public void Deve_Retornar_Um_Usuario()
        {
            mockRepositoryUsuario
                .Setup(x => x.ObterPrimeiroPor(u => u.Login == It.IsAny<string>() && u.Senha == It.IsAny<string>(), default))
                .ReturnsAsync(new Usuario { Id = Guid.Parse("8053841b-325c-4631-bf13-729df865d911") });

            _authService = new AuthService(mockRepositoryUsuario.Object, mockSecurityService.Object);

            Task.Run(async () =>
            {
                Guid usuarioId = await _authService.Autenticar("login", "123qwe");
                Assert.Equal(Guid.Parse("8053841b-325c-4631-bf13-729df865d911"), usuarioId);
            });

        }
    }
}
