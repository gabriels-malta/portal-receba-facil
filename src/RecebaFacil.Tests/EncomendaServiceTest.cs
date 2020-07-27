using Moq;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Services;
using RecebaFacil.Repository.Interfaces;
using RecebaFacil.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RecebaFacil.Tests
{
    public class EncomendaServiceTest
    {
        private IEncomendaService _encomendaService;

        private readonly Mock<IRepositoryEncomenda> mockRepositoryEncomenda = new Mock<IRepositoryEncomenda>();
        private readonly Mock<IEmpresaService> mockEmpresaService = new Mock<IEmpresaService>();


        [Fact]
        public void Deve_Salvar_uma_Nova_Encomenda()
        {
            mockEmpresaService
                .Setup(x => x.ExistePontoRetirada(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            mockEmpresaService
                .Setup(x => x.ExistePontoVenda(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            mockRepositoryEncomenda
                .Setup(x => x.Salvar(It.IsAny<Encomenda>()))
                .Verifiable();

            _encomendaService = new EncomendaService(mockRepositoryEncomenda.Object,
                                                     mockEmpresaService.Object,
                                                     null,
                                                     null);

            Encomenda encomenda = new Encomenda
            {
                PontoRetiradaId = Guid.NewGuid(),
                PontoVendaId = Guid.NewGuid(),
                DataPedido = DateTime.Now,
                NumeroPedido = "HUFHYGD-8956-UID87",
                NotaFiscal = "745230",
            };

            Task.Run(async () =>
            {
                await _encomendaService.Salvar(encomenda);

                Assert.Equal(
                    encomenda.Id,
                    encomenda.Historia.ElementAt(0).EncomendaId);

                Assert.Equal(
                    TipoMovimento.EsteiraIniciada,
                    encomenda.Historia.ElementAt(0).TipoMovimento);
            });

        }
    }
}
