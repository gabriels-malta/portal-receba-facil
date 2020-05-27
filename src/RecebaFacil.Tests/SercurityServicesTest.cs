using RecebaFacil.Domain.Services;
using RecebaFacil.Service;
using Xunit;

namespace RecebaFacil.Tests
{
    public class SercurityServicesTest : IClassFixture<SecurityService>
    {
        [Fact]
        public void Deve_Criptografar()
        {
            ISecurityService securityService = new SecurityService();
            string result = securityService.EncryptValue("texto puro para criptografar");
            Assert.Equal("4kusnSm5wcwPjh2wdMvvEv3RSTUJ/T1NzijSwa6ZVDE=", result);
        }


        [Fact]
        public void Deve_Descriptografar()
        {
            ISecurityService securityService = new SecurityService();
            string result = securityService.DecryptValue("4kusnSm5wcwPjh2wdMvvEv3RSTUJ/T1NzijSwa6ZVDE=");
            Assert.Equal("texto puro para criptografar", result);
        }

        [Fact]
        public void Deve_Calcular_Hash()
        {
            const string HASH = "fbfb386efea67e816f2dda0a8c94a98eb203757aebb3f55f183755a192d44467";
            ISecurityService securityService = new SecurityService();
            string hash = securityService.HashValue("123qwe");
            Assert.Equal(HASH, hash);
        }
    }
}
