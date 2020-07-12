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
    public class RepositoryEncomendaHistoria : RepositoryBase<EncomendaHistoria>, IRepositoryEncomendaHistoria
    {
        public RepositoryEncomendaHistoria(RFContext context) : base(context)
        {
        }

        public override Task<IList<EncomendaHistoria>> ObterTodos(CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public override async Task<IList<EncomendaHistoria>> ObterListaPor(Expression<Func<EncomendaHistoria, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _context.EncomendaHistoria
                .Where(expression)
                .OrderByDescending(x => x.DataCadastro)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
