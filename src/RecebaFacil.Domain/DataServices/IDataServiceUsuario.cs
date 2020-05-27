using System.Data;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceUsuario : IDataServiceBase<long>
    {
        long BuscarPorAutenticacao(string email, string senha);
    }
}
