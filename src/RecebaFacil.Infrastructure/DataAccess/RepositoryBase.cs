using Dapper;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public abstract class RepositoryBase<T>
    {
        private readonly SqlConnection _connection;

        protected RepositoryBase(ISqlAccess databaseHandler)
        {
            _connection = databaseHandler.CreateConnection();
        }

        protected IEnumerable<T> ExecuteToEnumerable(string commandText, object filter)
        {
            return _connection.Query<T>(commandText, filter, commandType: CommandType.StoredProcedure);
        }

        protected T ExecuteToFirstOrDefault(string commandText, object filter)
        {
            return _connection.QueryFirstOrDefault<T>(commandText, filter, commandType: CommandType.StoredProcedure);
        }

        protected int ExecuteNonQuery(string commandText, object entity)
        {
            return _connection.Execute(commandText, entity, commandType: CommandType.StoredProcedure);
        }

        protected object ExecuteScalar(string commandText, object entity)
        {
            return _connection.ExecuteScalar(commandText, entity, commandType: CommandType.StoredProcedure);
        }
    }
}
