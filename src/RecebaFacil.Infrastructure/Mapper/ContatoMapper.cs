using Dapper.FluentMap.Mapping;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Infrastructure.Mapper
{
    internal class ContatoMapper : EntityMap<Contato>
    {
        internal ContatoMapper()
        {
            Map(x => x.Id).ToColumn("intIdContato");
            Map(x => x.EmpresaID).ToColumn("intIdEmpresa");
            Map(x => x.Nome).ToColumn("vchNome");
            Map(x => x.Valor).ToColumn("vchValor");
            Map(x => x.TipoContato).ToColumn("tinCodTipoContato");
            Map(x => x.Ativo).ToColumn("bitAtivo");
        }
    }
}
