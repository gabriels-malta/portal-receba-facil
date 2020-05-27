using RecebaFacil.Domain.DataServices;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceGrupo : RepositoryBase, IDataServiceGrupo
    {
        public DataServiceGrupo(ISqlAccess sqlAccess)
            : base(sqlAccess)
        { }

        public DataSet ObterPorId(short id)
        {
            return ExecuteCommand("sproc_Grupo_ObterPorId", new SqlParameter[]
                {
                    new SqlParameter("@id", DbType.Int16) { Value = id }
                });
        }
    }
}
