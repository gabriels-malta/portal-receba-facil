using Microsoft.EntityFrameworkCore;
using RecebaFacil.Domain.Entities;
using RecebaFacil.Repository.ContextConfig;
using RecebaFacil.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RecebaFacil.Repository
{
    public class RepositoryEncomenda : RepositoryBase<Encomenda>, IRepositoryEncomenda
    {
        public RepositoryEncomenda(RFContext context)
            : base(context)
        {
        }
        public override async Task<IList<Encomenda>> ObterListaPor(Expression<Func<Encomenda, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Encomenda>()
                .Include(x => x.Historia)
                .Where(expression)
                .AsNoTracking()
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
