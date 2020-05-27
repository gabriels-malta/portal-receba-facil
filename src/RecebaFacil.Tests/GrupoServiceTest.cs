using Moq;
using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using RecebaFacil.Domain.Services;
using RecebaFacil.Infrastructure.DataAccess;
using RecebaFacil.Service;
using RecebaFacil.Service.Mappers;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit;

namespace RecebaFacil.Tests
{
    public class GrupoServiceTest
    {
        private IGrupoService _service;
        private IDataServiceGrupo _dataService;

        [Fact]
        public void Deve_Obter_Grupo_por_Id()
        {
            Mock<IDataServiceGrupo> _mockDataService = new Mock<IDataServiceGrupo>();
            _mockDataService.Setup(setup => setup.ObterPorId(3)).Returns(MockDataSet);
            _dataService = _mockDataService.Object;

            _service = new GrupoService(_dataService, new GrupoMapper());

            var resultado = _service.BuscarPorId(3);

            Assert.Equal("GRUPO_03", resultado.Role);
        }

        #region Mock methods
        private DataSet MockDataSet()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("tinIdGrupo",typeof(byte)),
                new DataColumn("vchNome", typeof(string)),
                new DataColumn("vchRole", typeof(string))
            });

            dataTable.Rows.Add(3, "Grupo 03", "GRUPO_03");

            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);
            return dataSet;
        }
        #endregion
    }
}
