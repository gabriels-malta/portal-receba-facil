using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System;
using System.Data;
using System.Data.SqlClient;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServicePreRegistro : RepositoryBase, IDataServicePreRegistro
    {
        public DataServicePreRegistro(ISqlAccess databaseHandler) 
            : base(databaseHandler)
        {
        }

        public DataSet ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Salvar(PreRegistro registro)
        {
            return ExecuteNonQuery("sproc_PreRegistro_Inserir", new SqlParameter[]
            {
                new SqlParameter("@uniId", Guid.NewGuid()),
                new SqlParameter("@vchNome", registro.Nome),
                new SqlParameter("@vchEmail", registro.Email),
                new SqlParameter("@vchTelefone", registro.Telefone),
                new SqlParameter("@vchNomeEmpresa", registro.NomeEmpresa),
                new SqlParameter("@vchCidade", registro.Cidade),
                new SqlParameter("@vchEndereco", registro.Endereco),
                new SqlParameter("@vchCnpj", registro.Cnpj),
                new SqlParameter("@chMotivo", registro.Objetivo)
            }); ;                  
        }
    }
}
