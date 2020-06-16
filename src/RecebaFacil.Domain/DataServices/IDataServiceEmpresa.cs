using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceEmpresa
    {
        Empresa ObterPorId(int id);
    }
}
