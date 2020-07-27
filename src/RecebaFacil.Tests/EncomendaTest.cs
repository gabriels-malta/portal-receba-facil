using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Exception;
using System;
using System.Linq;
using Xunit;

namespace RecebaFacil.Tests
{
    public class EncomendaTest
    {
        [Fact]
        public void Deve_Adicionar_Uma_Historia()
        {
            Encomenda encomenda = new Encomenda
            {
                PontoRetiradaId = Guid.NewGuid(),
                PontoVendaId = Guid.NewGuid(),
                DataPedido = DateTime.Now,
                NumeroPedido = "HUFHYGD-8956-UID87",
                NotaFiscal = "745230",
            };

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                DataCadastro = DateTime.Now,
                Id = Guid.NewGuid(),
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.EsteiraIniciada
            });

            Assert.Equal(encomenda.Id, encomenda.Historia.ElementAt(0).EncomendaId);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Ao_Adicionar_Uma_Historia()
        {
            Encomenda encomenda = new Encomenda
            {
                PontoRetiradaId = Guid.NewGuid(),
                PontoVendaId = Guid.NewGuid(),
                DataPedido = DateTime.Now,
                NumeroPedido = "HUFHYGD-8956-UID87",
                NotaFiscal = "745230",
            };

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                DataCadastro = DateTime.Now,
                Id = Guid.NewGuid(),
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.EsteiraIniciada
            });

            RecebaFacilException exception = Assert.Throws<RecebaFacilException>(() =>
            {
                encomenda.AdicionarHistoria(new EncomendaHistoria
                {
                    EncomendaId = encomenda.Id,
                    TipoMovimento = TipoMovimento.EsteiraIniciada
                });
            });

            Assert.Equal("Movimento não permitido para esta encomenda", exception.Message);
        }

        [Fact]
        public void Deve_Permitir_Movimentar_Esteira()
        {
            Encomenda encomenda = new Encomenda
            {
                PontoRetiradaId = Guid.NewGuid(),
                PontoVendaId = Guid.NewGuid(),
                DataPedido = DateTime.Now,
                NumeroPedido = "HUFHYGD-8956-UID87",
                NotaFiscal = "745230",
            };

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.EsteiraIniciada
            });

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.EnviadoPontoRetirada
            });

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.RecebidoPontoRetirada
            });


            Assert.True(encomenda.PodeMovimentar());
        }

        [Theory]
        [InlineData(TipoMovimento.RetiradoClienteFinal)]
        [InlineData(TipoMovimento.DevolvidoPontoVenda)]
        [InlineData(TipoMovimento.EsteiraFinalizada)]
        public void Nao_Deve_Permitir_Movimentar_Esteira(TipoMovimento movimento)
        {
            Encomenda encomenda = new Encomenda
            {
                PontoRetiradaId = Guid.NewGuid(),
                PontoVendaId = Guid.NewGuid(),
                DataPedido = DateTime.Now,
                NumeroPedido = "HUFHYGD-8956-UID87",
                NotaFiscal = "745230",
            };

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.EsteiraIniciada
            });

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.EnviadoPontoRetirada
            });

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = TipoMovimento.RecebidoPontoRetirada
            });

            encomenda.AdicionarHistoria(new EncomendaHistoria
            {
                EncomendaId = encomenda.Id,
                TipoMovimento = movimento
            });


            Assert.False(encomenda.PodeMovimentar());
        }
    }
}
