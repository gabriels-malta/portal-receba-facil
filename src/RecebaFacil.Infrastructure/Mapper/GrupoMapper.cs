using Dapper.FluentMap.Mapping;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Infrastructure.Mapper
{
    internal class GrupoMapper : EntityMap<Grupo>
    {
        internal GrupoMapper()
        {
            Map(x => x.Id).ToColumn("tinIdGrupo");
            Map(x => x.Nome).ToColumn("vchNome");
            Map(x => x.Role).ToColumn("vchRole");
        }
    }
}
