
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IContatoService
    {
        Contato ObterPorId(int id);
        Contato ObterPorId(int id, bool carregarEmpresa);
    }
}
