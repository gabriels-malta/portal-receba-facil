using RecebaFacil.Domain.DataServices;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceContato : RepositoryBase, IDataServiceContato
    {
        public DataServiceContato(ISqlAccess databaseHandler)
            : base(databaseHandler)
        {
        }

        public DataSet ObterPorId(int id)
        {
            return ExecuteCommand("sproc_Contato_ObterPorId", new SqlParameter[]
            {
                new SqlParameter("@id", DbType.Int64) { Value = id }
            });
        }
    }
}
