using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Exception;
using System;
using System.Globalization;
using Xunit;

namespace RecebaFacil.Tests
{
    public class EntityExpedienteTest
    {
        [Fact]
        public void Deve_Criar_Instancia_Expediente()
        {
            Guid id = Guid.NewGuid();
            Guid empresaId = Guid.Parse("942cce75-b118-4b4c-848f-a85131076e1e");
            TimeSpan abertura = TimeSpan.Parse("9:00", CultureInfo.InvariantCulture);
            TimeSpan encerramento = TimeSpan.Parse("18:00", CultureInfo.InvariantCulture);
            Expediente expediente = new Expediente
            {
                Id = id,
                PontoRetiradaId = empresaId,
                DiaSemana = DayOfWeek.Monday,
                HoraAbertura = abertura,
                HoraEncerramento = encerramento
            };

            Assert.Equal(DayOfWeek.Monday, Enum.Parse<DayOfWeek>(expediente.DiaSemana.ToString()));
        }

        [Fact]
        public void Expediente_Com_HorarioFuncionamento_Invalido()
        {
            Guid id = Guid.NewGuid();
            Guid empresaId = Guid.NewGuid();
            TimeSpan abertura = TimeSpan.Parse("20:00", CultureInfo.InvariantCulture);
            TimeSpan encerramento = TimeSpan.Parse("18:00", CultureInfo.InvariantCulture);

            Expediente expediente = new Expediente
            {
                Id = id,
                PontoRetiradaId = empresaId,
                DiaSemana = DayOfWeek.Monday,
                HoraAbertura = abertura,
                HoraEncerramento = encerramento
            };
            RecebaFacilException exception = Assert.Throws<RecebaFacilException>(() =>
            {
                expediente.ValidarHorarios();
            });

            Assert.Equal("Horário de funcionamento inválido", exception.Message);
        }

        [Fact]
        public void Expediente_Com_DiaSemana_Invalido()
        {
            Guid id = Guid.NewGuid();
            Guid empresaId = Guid.NewGuid();
            byte diaSemana = 9; // Fora dos valores do enum DayOfWeek
            TimeSpan abertura = TimeSpan.Parse("9:00", CultureInfo.InvariantCulture);
            TimeSpan encerramento = TimeSpan.Parse("18:00", CultureInfo.InvariantCulture);
            Expediente expediente = new Expediente
            {
                Id = id,
                PontoRetiradaId = empresaId,
                DiaSemana = (DayOfWeek)diaSemana,
                HoraAbertura = abertura,
                HoraEncerramento = encerramento
            };

            RecebaFacilException exception = Assert.Throws<RecebaFacilException>(() =>
            {
                expediente.ValidarDiaSemana();
            });

            Assert.Equal("Dia da semana inválido", exception.Message);
        }
    }
}
