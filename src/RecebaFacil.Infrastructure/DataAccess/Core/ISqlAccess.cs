using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess.Core
{
    public interface ISqlAccess
    {
        SqlConnection CreateConnection();
        SqlCommand CreateCommand(string commandText, CommandType type, SqlConnection connection);
        SqlDataAdapter CreateAdapter(SqlCommand command);
    }
}
