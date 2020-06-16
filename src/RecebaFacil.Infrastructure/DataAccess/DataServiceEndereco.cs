using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceEndereco : RepositoryBase<Endereco>, IDataServiceEndereco
    {
        public DataServiceEndereco(ISqlAccess databaseHandler)
            : base(databaseHandler)
        { }

        public Endereco ObterEnderecoPorEmpresa(int empresaId, bool somentePrincipal)
        {
            return ExecuteToFirstOrDefault("sproc_Endereco_ObterPorEmpresa", new
            {
                intIdEmpresa = empresaId,
                bitSomentePrincipal = somentePrincipal
            });
        }

        public Endereco ObterPorId(int id)
        {
            return ExecuteToFirstOrDefault("sproc_Endereco_ObterPorId", new { id });
        }

        public int Salvar(Endereco endereco)
        {
            return ExecuteNonQuery("sproc_Endereco_Inserir", new
            {
                intIdEmpresa = endereco.EmpresaId,
                vchCep = endereco.Cep,
                vchLogradouro = endereco.Logradouro,
                vchBairro = endereco.Bairro,
                vchMunicipio = endereco.Municipio,
                vchUf = endereco.Uf,
                vchObservacao = endereco.Observacao
            });
        }
    }
}
