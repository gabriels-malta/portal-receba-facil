using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceEndereco : RepositoryBase, IDataServiceEndereco
    {
        public DataServiceEndereco(ISqlAccess databaseHandler)
            : base(databaseHandler)
        { }

        public DataSet ObterEnderecoPorEmpresa(int empresaId, bool somentePrincipal)
        {
            return ExecuteCommand("sproc_Endereco_ObterPorEmpresa", new SqlParameter[]
            {
                new SqlParameter("@intIdEmpresa", DbType.Int32) { Value = empresaId },
                new SqlParameter("@bitSomentePrincipal", DbType.Boolean) { Value = somentePrincipal },
            });
        }

        public DataSet ObterPorId(int id)
        {
            return ExecuteCommand("sproc_Endereco_ObterPorId", new SqlParameter[]
            {
                new SqlParameter("@id", DbType.Int32 ) { Value = id }
            });
        }

        public int Salvar(Endereco endereco)
        {
            return ExecuteNonQuery("sproc_Endereco_Inserir", new SqlParameter[]
            {
                new SqlParameter("@intIdEmpresa", DbType.Int32) { Value = endereco.EmpresaId },
                new SqlParameter("@vchCep", DbType.String) { Value = endereco.Cep, Size = 8 },
                new SqlParameter("@vchLogradouro", DbType.String) { Value = endereco.Logradouro, Size = 100},
                new SqlParameter("@vchBairro", DbType.String) { Value = endereco.Bairro, Size = 50 },
                new SqlParameter("@vchMunicipio", DbType.String) { Value = endereco.Municipio, Size = 80 },
                new SqlParameter("@vchUf", DbType.String) { Value = endereco.Uf, Size = 2 },
                new SqlParameter("@vchObservacao", DbType.String) { Value = endereco.Observacao, Size = 255 }
            });
        }
    }
}
