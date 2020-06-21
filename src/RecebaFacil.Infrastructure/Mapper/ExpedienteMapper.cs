using Dapper.FluentMap.Mapping;
using RecebaFacil.Domain.Entities;

namespace RecebaFacil.Infrastructure.Mapper
{
    internal class ExpedienteMapper : EntityMap<Expediente>
    {
        internal ExpedienteMapper()
        {
            Map(x => x.Id).ToColumn("uniIdExpediente");
            Map(x => x.PontoRetiradaId).ToColumn("intIdEmpresa");
            Map(x => x.DiaSemana).ToColumn("tinDiaSemana");
            Map(x => x.HoraAbertura).ToColumn("tmHoraAbertura");
            Map(x => x.HoraEncerramento).ToColumn("tmHoraEncerramento");
        }
    }
}
