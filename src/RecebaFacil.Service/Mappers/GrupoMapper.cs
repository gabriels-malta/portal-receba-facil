using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Mappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service.Mappers
{
    public class GrupoMapper : IGrupoMapper
    {
        public Grupo Map(DataRow row)
        {
            return new Grupo
            {
                Id = row.Field<byte>("tinIdGrupo"),
                Nome = row.Field<string>("vchNome"),
                Role = row.Field<string>("vchRole")
            };
        }

        public IEnumerable<Grupo> Map(IEnumerable<DataRow> rows)
        {
            return rows.Select(x => Map(x));
        }
    }
}
