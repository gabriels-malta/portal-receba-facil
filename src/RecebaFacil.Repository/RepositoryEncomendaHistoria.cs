using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;

namespace RecebaFacil.Repository
{
    public class RepositoryEncomendaHistoria : RepositoryBase<EncomendaHistoria>, IRepositoryEncomendaHistoria
    {
        public RepositoryEncomendaHistoria(RFContext context) : base(context)
        {
        }
    }
}
