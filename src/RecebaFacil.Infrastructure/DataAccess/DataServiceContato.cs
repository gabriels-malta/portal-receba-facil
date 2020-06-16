using RecebaFacil.Domain.DataServices;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Infrastructure.DataAccess.Core;

namespace RecebaFacil.Infrastructure.DataAccess
{
    public class DataServiceContato : RepositoryBase<Contato>, IDataServiceContato
    {
        public DataServiceContato(ISqlAccess databaseHandler)
            : base(databaseHandler)
        {
        }

        public Contato ObterPorId(int id)
        {
            return ExecuteToFirstOrDefault("sproc_Contato_ObterPorId", new { id });
        }
    }
}
