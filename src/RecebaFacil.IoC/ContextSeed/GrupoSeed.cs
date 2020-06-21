using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.IoC.ContextSeed
{
    public class GrupoSeed : ISeedService
    {
        private readonly IRepositoryBase<Grupo> _repository;

        public GrupoSeed(IRepositoryBase<Grupo> repository)
        {
            _repository = repository;
        }

        public async Task Feed()
        {
            foreach (Grupo grupo in CriarGrupos())
            {
                await _repository.Salvar(grupo);
            }
        }

        private IEnumerable<Grupo> CriarGrupos()
        {
            yield return new Grupo
            {
                Id = Guid.Parse("44edaf28-785d-49a7-a798-8b1893be19d0"),
                Nome = "Ponto de Retirada",
                Role = "PONTO_RETIRADA"
            };

            yield return new Grupo
            {
                Id = Guid.Parse("d010bd9d-b922-4cc5-a3fe-cb37bd418802"),
                Nome = "Ponto de Venda",
                Role = "PONTO_VENDA"
            };
        }
    }

    public class ExpedienteSeed : ISeedService
    {
        private readonly IRepositoryExpediente _repository;

        public ExpedienteSeed(IRepositoryExpediente repository)
        {
            _repository = repository;
        }

        public async Task Feed()
        {
            foreach (Expediente expediente in CriarExpedientes())
            {
                await _repository.Salvar(expediente);
            }
        }

        private IEnumerable<Expediente> CriarExpedientes()
        {
            Guid pontoRetiradaId = Guid.Parse("abe3dbb7-baaf-4699-a568-6c6bb21f7d0b");
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(9, 0, 0),
                HoraEncerramento = new TimeSpan(20, 0, 0),
                DiaSemana = DayOfWeek.Monday
            };
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(9, 0, 0),
                HoraEncerramento = new TimeSpan(20, 0, 0),
                DiaSemana = DayOfWeek.Tuesday
            };
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(9, 0, 0),
                HoraEncerramento = new TimeSpan(20, 0, 0),
                DiaSemana = DayOfWeek.Wednesday
            };
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(9, 0, 0),
                HoraEncerramento = new TimeSpan(20, 0, 0),
                DiaSemana = DayOfWeek.Thursday
            };
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(9, 0, 0),
                HoraEncerramento = new TimeSpan(20, 0, 0),
                DiaSemana = DayOfWeek.Friday
            };
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(10, 0, 0),
                HoraEncerramento = new TimeSpan(18, 0, 0),
                DiaSemana = DayOfWeek.Saturday
            };
            yield return new Expediente
            {
                Id = Guid.NewGuid(),
                PontoRetiradaId = pontoRetiradaId,
                HoraAbertura = new TimeSpan(10, 0, 0),
                HoraEncerramento = new TimeSpan(14, 0, 0),
                DiaSemana = DayOfWeek.Sunday
            };
        }
    }
}
