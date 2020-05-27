using System.Collections.Generic;
using System.Data;

namespace RecebaFacil.Domain.Mappers
{
    public interface IMapperBase<TEntity>
    {
        TEntity Map(DataRow row);
        IEnumerable<TEntity> Map(IEnumerable<DataRow> rows);
    }
}
