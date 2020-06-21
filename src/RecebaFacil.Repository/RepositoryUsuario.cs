using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;

namespace RecebaFacil.Repository
{
    public class RepositoryUsuario : RepositoryBase<Usuario>, IRepositoryUsuario
    {
        public RepositoryUsuario(RFContext context)
            : base(context)
        {
        }
    }
}
