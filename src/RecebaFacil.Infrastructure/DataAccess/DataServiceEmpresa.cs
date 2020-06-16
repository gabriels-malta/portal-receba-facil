using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceEmpresa : RepositoryBase<Empresa>, IDataServiceEmpresa
    {
        public DataServiceEmpresa(ISqlAccess databaseHandler)
            : base(databaseHandler)
        { }

        public Empresa ObterPorId(int id)
        {
            return ExecuteToFirstOrDefault("sproc_Empresa_ObterPorId", new { id });
        }
    }
}
