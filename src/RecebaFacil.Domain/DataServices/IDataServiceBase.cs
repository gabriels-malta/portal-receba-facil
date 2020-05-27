using System.Data;

namespace RecebaFacil.Domain.DataServices
{
    public interface IDataServiceBase<TId>
    {
        DataSet ObterPorId(TId id);
    }
}
