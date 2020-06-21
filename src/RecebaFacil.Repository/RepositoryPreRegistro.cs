using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;

namespace RecebaFacil.Repository
{
    public class RepositoryPreRegistro : RepositoryBase<PreRegistro>, IRepositoryPreRegistro
    {
        public RepositoryPreRegistro(RFContext context) : base(context)
        {
        }
    }
}
