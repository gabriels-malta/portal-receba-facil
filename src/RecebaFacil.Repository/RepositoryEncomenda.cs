using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;

namespace RecebaFacil.Repository
{
    public class RepositoryEncomenda : RepositoryBase<Encomenda>, IRepositoryEncomenda
    {
        public RepositoryEncomenda(RFContext context) : base(context)
        {
        }
    }
}
