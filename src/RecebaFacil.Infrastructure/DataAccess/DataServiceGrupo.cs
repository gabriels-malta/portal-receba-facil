using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceGrupo : RepositoryBase<Grupo>, IDataServiceGrupo
    {
        public DataServiceGrupo(ISqlAccess sqlAccess)
            : base(sqlAccess)
        { }

        public Grupo ObterPorId(short id)
        {
            return ExecuteToFirstOrDefault("sproc_Grupo_ObterPorId", new { id });
        }
    }
}
