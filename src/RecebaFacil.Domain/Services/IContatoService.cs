
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IContatoService : IServiceBase<Contato, int>
    {
        Contato BuscarPorId(int id, bool carregarEmpresa);
    }
}
