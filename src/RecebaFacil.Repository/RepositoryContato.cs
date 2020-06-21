using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;

namespace RecebaFacil.Repository
{
    public class RepositoryContato : RepositoryBase<Contato>, IRepositoryContato
    {
        public RepositoryContato(RFContext context) : base(context)
        { }
    }
}
