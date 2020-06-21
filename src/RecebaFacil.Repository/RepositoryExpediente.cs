using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;

namespace RecebaFacil.Repository
{
    public class RepositoryExpediente : RepositoryBase<Expediente>, IRepositoryExpediente
    {
        public RepositoryExpediente(RFContext context): base(context)
        {
        }
    }
}
