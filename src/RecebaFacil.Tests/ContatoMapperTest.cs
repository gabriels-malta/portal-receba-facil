using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Service.Mappers;
using System.Data;
using System.Linq;
using Xunit;

namespace RecebaFacil.Tests
{
    public class ContatoMapperTest
    {
        [Fact]
        public void Deve_Retornar_Null_Quando_Nao_Encontrar_Um_Contato()
        {
            //Arrange
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("intIdContato"),
                new DataColumn("intIdEmpresa"),
                new DataColumn("vchNome"),
                new DataColumn("vchValor"),
                new DataColumn("tinCodTipoContato"),
                new DataColumn("bitAtivo"),
            });

            //Act
            IContatoMapper contatoMapper = new ContatoMapper();
            Contato contato = contatoMapper.Map(dataTable.AsEnumerable()).FirstOrDefault();

            //Assert
            Assert.Null(contato);
        }
    }
}
