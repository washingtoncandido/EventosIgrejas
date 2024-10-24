﻿using FICR.Cooperation.Humanism.Entities.Base;
using FICR.Cooperation.Humanism.Entities.Pagination;
using System.Linq.Expressions;

namespace FICR.Cooperation.Humanism.Data.Contracts.Base
{
    public interface IRepository<TEntity>
    where TEntity : BaseEntity, new()
    {
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> FindAsync(TEntity entity, bool includeDeleted = false, bool eager = false);
        Task<IEnumerable<TEntity>> FindAllAsync(bool includeDeleted = false);
        Task<TEntity> FindByIdAsync(Guid id, bool includeDeleted = false);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, bool includeDeleted = false, bool eager = false);
        TEntity Delete(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity UnDelete(Guid id);
        Task<PagedResult<TEntity>> GetActiveElementsPagedResultAsync(PaginationFilter filter);
        Task<PagedResult<TEntity>> FindAllByPagedAsync(PaginationFilter filter, bool includeDeleted = false);
    }
}
