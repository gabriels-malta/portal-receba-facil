
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IUsuarioService
    {
        Usuario ObterPorId(long id);
        Usuario BuscarPorAutenticacao(string login, string senha);
    }
}
