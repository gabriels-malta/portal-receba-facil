using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceContato
    {
        Contato ObterPorId(int id);
    }
}
