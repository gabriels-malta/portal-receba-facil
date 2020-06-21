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
            yield return new Grupo
            {
                Id = Guid.Parse("8bfe7e07-52d4-420b-a349-0037d98a84ef"),
                Nome = "Administrador",
                Role = "ADMINISTRADOR"
            };
        }
    }
}
