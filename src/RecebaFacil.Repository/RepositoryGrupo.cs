using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;

namespace RecebaFacil.Repository
{
    public class RepositoryGrupo : RepositoryBase<Grupo>, IRepositoryGrupo
    {
        public RepositoryGrupo(RFContext context) : base(context)
        {
        }
    }
}
