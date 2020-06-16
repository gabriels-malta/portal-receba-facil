using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess.Core
{
    public interface ISqlAccess
    {
        SqlConnection CreateConnection();
    }
}
