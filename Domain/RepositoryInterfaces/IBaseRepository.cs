using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shared.Helpers;

namespace Domain.RepositoryInterfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        IQueryable<T> Query();

        // Get
        Task<T> GetByIdAsync(Guid id, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default);
        Task<IEnumerable<T>> GetAllAsync(
            bool trackChanges = false,
            List<Expression<Func<T, object>>>? includes = null,
            CancellationToken ct = default);

        // Filtering
        Task<IEnumerable<T>> FindRangeAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            List<Expression<Func<T, object>>>? includes = null, 
            CancellationToken ct = default);
        Task<T> FindAsync(
            Expression<Func<T, bool>> predicate,
            bool trackChanges = false,
            List<Expression<Func<T, object>>>? includes = null,
            CancellationToken ct = default);

        // Paging & Counting
        Task<PagedList<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
              List<Expression<Func<T, object>>>? includes = null,
            CancellationToken ct = default);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default);

        Task<bool> Exist(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default);

        // CRUD
        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Remove(T entity); // hard delete
        void SoftDelete(T entity); // soft delete
        void RemoveRange(IEnumerable<T> entities);
    }
}
