using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceUsuario
    {
        Usuario ObterPorId(long id);
        long BuscarPorAutenticacao(string email, string senha);
    }
}
