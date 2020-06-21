using Dapper.FluentMap.Mapping;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Infrastructure.Mapper
{
    internal class EnderecoMapper : EntityMap<Endereco>
    {
        internal EnderecoMapper()
        {
            Map(x => x.Id).ToColumn("intIdEndereco");
            Map(x => x.EmpresaId).ToColumn("intIdEmpresa");
            Map(x => x.Cep).ToColumn("vchCep");
            Map(x => x.Logradouro).ToColumn("vchLogradouro");
            Map(x => x.Bairro).ToColumn("vchBairro");
            Map(x => x.Municipio).ToColumn("vchMunicipio");
            Map(x => x.Uf).ToColumn("vchUf");
            Map(x => x.Observacao).ToColumn("vchObservacao");
            Map(x => x.Ativo).ToColumn("bitAtivo");
        }
    }
}
