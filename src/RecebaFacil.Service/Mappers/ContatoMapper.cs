using RecebaFacil.Domain.Entities;
using RecebaFacil.Domain.Enums;
using RecebaFacil.Domain.Mappers;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace RecebaFacil.Service.Mappers
{
    public class ContatoMapper : IContatoMapper
    {
        public Contato Map(DataRow row)
        {
            return new Contato
            {
                Id = row.Field<int>("intIdContato"),
                EmpresaID = row.Field<int>("intIdEmpresa"),
                Nome = row.Field<string>("vchNome"),
                Valor = row.Field<string>("vchValor"),
                TipoContato = (TipoContato)row.Field<byte>("tinCodTipoContato"),
                Ativo = row.Field<bool>("bitAtivo")
            };
        }

        public IEnumerable<Contato> Map(IEnumerable<DataRow> rows)
        {
            return rows.Select(x => Map(x));
        }
    }
}
