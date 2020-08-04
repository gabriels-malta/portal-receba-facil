using RecebaFacil.Domain.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace RecebaFacil.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity>
        where TEntity : class, IEntityBase
    {
        Task<Guid> Salvar(TEntity entity);

        Task Atualizar(TEntity entity);

        Task Excluir(TEntity entity);

        ValueTask<TEntity> ObterPorId(Guid id, CancellationToken cancellationToken = default);

        Task<TEntity> ObterPrimeiroPor(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<IList<TEntity>> ObterListaPor(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        ValueTask<bool> Existe(Expression<Func<TEntity, bool>> expression);
    }
}
