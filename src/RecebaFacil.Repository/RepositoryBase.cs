using Microsoft.EntityFrameworkCore;
using RecebaFacil.Domain.Core.BaseEntities;
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
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class, IEntityBase
    {
        protected readonly RFContext _context;

        public RepositoryBase(RFContext context)
        {
            _context = context;
        }

        public Task Atualizar(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<TEntity>().Update(entity);

            return _context.SaveChangesAsync();
        }

        public Task Excluir(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            return _context.SaveChangesAsync();
        }

        public virtual async ValueTask<bool> Existe(Expression<Func<TEntity, bool>> expression)
        {
            return await _context.Set<TEntity>()
                .Where(expression)
                .AnyAsync();
        }

        public virtual async Task<IList<TEntity>> ObterListaPor(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>()
                .Where(expression)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async ValueTask<TEntity> ObterPorId(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> ObterPrimeiroPor(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task<Guid> Salvar(TEntity entity)
        {
            await _context
                .Set<TEntity>()
                .AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
