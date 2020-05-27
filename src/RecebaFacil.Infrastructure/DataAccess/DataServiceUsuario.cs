using RecebaFacil.Domain.DataServices;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceUsuario : RepositoryBase, IDataServiceUsuario
    {
        public DataServiceUsuario(ISqlAccess sqlAccess)
            : base(sqlAccess)
        { }

        public long BuscarPorAutenticacao(string email, string senha)
        {
            return Convert.ToInt64(ExecuteScalar("sproc_Usuario_ObterPorAutenticacao", new SqlParameter[]
            {
                new SqlParameter("@login", DbType.String) { Value = email, Size = 80 },
                new SqlParameter("@senha", DbType.String) { Value = senha, Size = 80 },
            }));
        }

        public DataSet ObterPorId(long id)
        {
            return ExecuteCommand("sproc_Usuario_ObterPorId", new SqlParameter[]
            {
                new SqlParameter("@id", DbType.Int64) { Value = id }
            });
        }
    }
}
