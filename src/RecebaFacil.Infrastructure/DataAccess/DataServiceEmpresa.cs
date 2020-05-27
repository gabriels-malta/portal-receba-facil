using RecebaFacil.Domain.DataServices;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceEmpresa : RepositoryBase, IDataServiceEmpresa
    {
        public DataServiceEmpresa(ISqlAccess databaseHandler)
            : base(databaseHandler)
        { }

        public DataSet ObterPorId(int id)
        {
            return ExecuteCommand("sproc_Empresa_ObterPorId", new SqlParameter[]
            {
                new SqlParameter("@id", DbType.Int64) { Value = id }
            });
        }
    }
}
