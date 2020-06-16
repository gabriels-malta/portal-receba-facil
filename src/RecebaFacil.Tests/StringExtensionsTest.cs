using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using Xunit;

namespace RecebaFacil.Tests
{
    public class StringExtensionsTest
    {
        [Theory]
        [InlineData("11988953285", TipoContato.Celular, "(11) 98895-3285")]
        [InlineData("1136378035", TipoContato.TelefoneFixo, "(11) 3637-8035")]
        [InlineData("contato@jorgeemayapizzariadeliveryltda.com.br", TipoContato.Email, "contato@jorgeemayapizzariadeliveryltda.com.br")]
        [InlineData("0800567123", TipoContato.URA, "0800567123")]
        public void Deve_Formatar_ContatoValor_De_Acordo_Com_Tipo(string valor, TipoContato tipo, string valorEsperado)
        {
            Contato contato = new Contato
            {
                Valor = valor,
                TipoContato = tipo
            };

            Assert.Equal(contato.Valor, valorEsperado);
        }

        [Fact]
        public void Deve_Formatar_Um_CNPJ()
        {
            Empresa empresa = new PontoRetirada("razao social", "nome fantasia", "45278850000194");

            Assert.Equal("45.278.850/0001-94", empresa.Cnpj);

        }
    }
}
