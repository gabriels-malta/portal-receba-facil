using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;

namespace RecebaFacil.Repository
{
    public class RepositoryEmpresa : RepositoryBase<Empresa>, IRepositoryEmpresa
    {
        public RepositoryEmpresa(RFContext context) : base(context)
        {
        }
    }
}
