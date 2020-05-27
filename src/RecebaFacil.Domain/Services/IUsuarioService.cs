
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.Services
{
    public interface IUsuarioService : IServiceBase<Usuario, long>
    {
        Usuario BuscarPorAutenticacao(string login, string senha);
    }
}
