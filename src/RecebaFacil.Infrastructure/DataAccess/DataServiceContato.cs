using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceContato : IDataServiceContato
    {
        private readonly IRepositoryBase<Contato, int> _repository;

        public DataServiceContato(IRepositoryBase<Contato, int> repository)
        {
            _repository = repository;
        }

        public async Task Excluir(int id)
        {
            Contato contato = await _repository.ObterPorId(id);
            await _repository.Excluir(contato);
        }

        public async Task<Contato> ObterPorId(int id) => await _repository.ObterPorId(id);

        public async Task<IList<Contato>> ObterTodos() => await _repository.ObterTodos();

        public async Task Salvar(Contato contato)
        {
            if (contato.Id > 0)
            {
                await _repository.Atualizar(contato);
            }
            else
            {
                await _repository.Salvar(contato);
            }
        }
    }
}
