using Dapper.FluentMap.Mapping;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Infrastructure.Mapper
{
    internal class UsuarioMapper : EntityMap<Usuario>
    {
        internal UsuarioMapper()
        {
            Map(x => x.Id).ToColumn("intIdUsuario");
            Map(x => x.Login).ToColumn("vchLogin");
            Map(x => x.GrupoId).ToColumn("tinCodGrupo");
            Map(x => x.Bloqueado).ToColumn("bitBloqueado");
            Map(x => x.TrocarSenha).ToColumn("bitDeveAlterarSenha");
            Map(x => x.ContatoId).ToColumn("intIdContato");
        }
    }
}
