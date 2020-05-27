using Moq;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Service.Mappers;
using System.Data;
using System.Linq;
using Xunit;

namespace RecebaFacil.Tests
{
    public class UsuarioMapperTest : IClassFixture<UsuarioMapper>
    {
        private IUsuarioMapper _usuarioMapper;
        private Mock<IDataServiceUsuario> _mockUsuarioDataService;


        [Fact]
        public void Deve_Mapear_um_DataService_Para_Usuario()
        {
            // Arrange
            Usuario resultadoEsperado = new Usuario() { Id = 1, Login = "usuario" };
            _mockUsuarioDataService = new Mock<IDataServiceUsuario>();
            _mockUsuarioDataService.Setup(setup => setup.ObterPorId(1)).Returns(MockDataSet);
            IDataServiceUsuario dataServiceUsuario = _mockUsuarioDataService.Object;
            _usuarioMapper = new UsuarioMapper();

            // Act
            Usuario resultado = _usuarioMapper.Map(dataServiceUsuario.ObterPorId(1).Tables[0].AsEnumerable()).FirstOrDefault();

            // Assert
            Assert.Equal(new { resultadoEsperado.Id, resultadoEsperado.Login }, new { resultado.Id, resultado.Login });

            _mockUsuarioDataService.Verify(x => x.ObterPorId(1), Times.Once);

        }

        private DataSet MockDataSet()
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("intIdUsuario", typeof(int)),
                new DataColumn("vchLogin", typeof(string)),
                new DataColumn("tinCodGrupo", typeof(byte)),
                new DataColumn("bitBloqueado", typeof(bool)),
                new DataColumn("bitDeveAlterarSenha", typeof(bool)),
                new DataColumn("intIdContato", typeof(object))
            });

            table.Rows.Add(1, "usuario", 3, false, false, null);

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(table);
            return dataSet;
        }
    }
}
