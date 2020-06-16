using Microsoft.Extensions.Configuration;
using RecebaFacil.Infrastructure.Mapper;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess.Core
{
    public class SqlAcess : ISqlAccess
    {
        private readonly string _connectionString;

        public SqlAcess(IConfiguration configuration, FluentMapperCofiguration mapperCofiguration)
        {
            _connectionString = configuration.GetConnectionString("JupiterConnection");
            mapperCofiguration.Initialize();
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
