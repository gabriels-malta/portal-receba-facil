using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;

namespace RecebaFacil.Repository
{
    public class RepositoryEndereco : RepositoryBase<Endereco>, IRepositoryEndereco
    {
        public RepositoryEndereco(RFContext context) : base(context)
        {
        }
    }
}
