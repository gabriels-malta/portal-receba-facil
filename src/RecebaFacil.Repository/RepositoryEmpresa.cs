using Microsoft.EntityFrameworkCore;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecebaFacil.Repository
{
    public class RepositoryEmpresa : RepositoryBase<Empresa>, IRepositoryEmpresa
    {
        public RepositoryEmpresa(RFContext context) : base(context)
        {
        }

        public async override ValueTask<Empresa> ObterPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Empresa
                .Include(x => x.Contatos)
                .FirstAsync(x => x.Id == id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
