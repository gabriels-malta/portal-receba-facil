using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;
using System;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServicePreRegistro : RepositoryBase<PreRegistro>, IDataServicePreRegistro
    {
        public DataServicePreRegistro(ISqlAccess databaseHandler)
            : base(databaseHandler)
        {
        }

        public PreRegistro ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Salvar(PreRegistro registro)
        {
            return ExecuteNonQuery("sproc_PreRegistro_Inserir", new
            {
                uniId = Guid.NewGuid(),
                vchNome = registro.Nome,
                vchEmail = registro.Email,
                vchTelefone = registro.Telefone,
                vchNomeEmpresa = registro.NomeEmpresa,
                vchCidade = registro.Cidade,
                vchEndereco = registro.Endereco,
                vchCnpj = registro.Cnpj,
                chMotivo = registro.Objetivo
            });
        }
    }
}
