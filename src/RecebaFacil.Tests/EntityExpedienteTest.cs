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
            int empresaId = 1;
            byte diaSemana = 1;
            TimeSpan abertura = TimeSpan.Parse("9:00", CultureInfo.InvariantCulture);
            TimeSpan encerramento = TimeSpan.Parse("18:00", CultureInfo.InvariantCulture);
            Expediente expediente = new Expediente(id, empresaId, diaSemana, abertura, encerramento);

            Assert.Equal(DayOfWeek.Monday, Enum.Parse<DayOfWeek>(expediente.DiaSemana.ToString()));
        }

        [Fact]
        public void Expediente_Com_HorarioFuncionamento_Invalido()
        {
            Guid id = Guid.NewGuid();
            int empresaId = 1;
            byte diaSemana = 1;
            TimeSpan abertura = TimeSpan.Parse("20:00", CultureInfo.InvariantCulture);
            TimeSpan encerramento = TimeSpan.Parse("18:00", CultureInfo.InvariantCulture);

            RecebaFacilException exception = Assert.Throws<RecebaFacilException>(() =>
            {
                Expediente expediente = new Expediente(id, empresaId, diaSemana, abertura, encerramento);
            });

            Assert.Equal("Horário de funcionamento inválido", exception.Message);
        }

        [Fact]
        public void Expediente_Com_DiaSemana_Invalido()
        {
            Guid id = Guid.NewGuid();
            int empresaId = 1;
            byte diaSemana = 9; // Fora dos valores do enum DayOfWeek
            TimeSpan abertura = TimeSpan.Parse("9:00", CultureInfo.InvariantCulture);
            TimeSpan encerramento = TimeSpan.Parse("18:00", CultureInfo.InvariantCulture);

            RecebaFacilException exception = Assert.Throws<RecebaFacilException>(() =>
            {
                Expediente expediente = new Expediente(id, empresaId, diaSemana, abertura, encerramento);
            });

            Assert.Equal("Dia da semana inválido", exception.Message);
        }
    }
}
