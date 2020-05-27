using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service.Mappers
{
    public class UsuarioMapper : IUsuarioMapper
    {
        public Usuario Map(DataRow row)
        {
            return new Usuario
            {
                Id = row.Field<int>("intIdUsuario"),
                Login = row.Field<string>("vchLogin"),
                GrupoId = row.Field<byte>("tinCodGrupo"),
                Bloqueado = row.Field<bool>("bitBloqueado"),
                TrocarSenha = row.Field<bool>("bitDeveAlterarSenha"),
                ContatoId = row.Field<int?>("intIdContato")
            };
        }

        public IEnumerable<Usuario> Map(IEnumerable<DataRow> rows)
        {
            return rows.Select(x => Map(x));
        }
    }
}
