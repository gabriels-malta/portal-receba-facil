using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess.Core
{
    public class SqlAcess : ISqlAccess
    {
        private readonly string _connectionString;

        public SqlAcess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("JupiterConnection");
        }

        public SqlDataAdapter CreateAdapter(SqlCommand command)
        {
            return new SqlDataAdapter(command);
        }

        public SqlCommand CreateCommand(string commandText, CommandType type, SqlConnection connection)
        {
            return new SqlCommand
            {
                CommandText = commandText,
                CommandType = type,
                Connection = connection
            };
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
