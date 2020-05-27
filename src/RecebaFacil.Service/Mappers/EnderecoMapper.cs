using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service.Mappers
{
    public class EnderecoMapper : IEnderecoMapper
    {
        public Endereco Map(DataRow row)
        {
            return new Endereco
                (
                enderecoId: row.Field<int>("intIdEndereco"),
                empresaId: row.Field<int>("intIdEmpresa"),
                cep: row.Field<string>("vchCep"),
                logradouro: row.Field<string>("vchLogradouro"),
                bairro: row.Field<string>("vchBairro"),
                municipio: row.Field<string>("vchMunicipio"),
                uf: row.Field<string>("vchUf"),
                observacao: row.Field<string>("vchObservacao"),
                principal: row.Field<bool>("bitPrincipal"),
                ativo: row.Field<bool>("bitAtivo")
                );
        }

        public IEnumerable<Endereco> Map(IEnumerable<DataRow> rows)
        {
            return rows.Select(x => Map(x));
        }
    }
}
