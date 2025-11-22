using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistant.Repositories
{
    internal abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly RepositoryDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(RepositoryDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> Query() => _dbSet.AsQueryable();

        public async Task<T> GetByIdAsync(Guid id, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            AddIncudes(ref query, includes);
            var entity = await query.FirstOrDefaultAsync(e => e.Id == id, ct).ConfigureAwait(false);
            return entity??throw new EntityNotFoundException(_dbSet.EntityType.Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges = false, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            AddIncudes(ref query, includes);

            return  trackChanges ?
                await query.AsTracking().ToListAsync(ct).ConfigureAwait(false):
                await query.AsNoTracking().ToListAsync(ct).ConfigureAwait(false);

        }
        public async Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            AddIncudes(ref query, includes);

            return    trackChanges ?
                await query.AsTracking().Where(predicate).ToListAsync(ct).ConfigureAwait(false)
              : await query.AsNoTracking().Where(predicate).ToListAsync(ct).ConfigureAwait(false);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            AddIncudes(ref query, includes);

            var entity =   trackChanges ?
                await query.AsTracking().FirstOrDefaultAsync(predicate, ct).ConfigureAwait(false)
              : await query.AsNoTracking().FirstOrDefaultAsync(predicate, ct).ConfigureAwait(false);

            return entity ?? throw new EntityNotFoundException(_dbSet.EntityType.Name);

        }
        public async Task<PagedList<T>> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object>>>? includes = null,
            CancellationToken ct = default)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > 50) pageSize = 50;
            
            IQueryable<T> query = _dbSet.AsQueryable();
            AddIncudes(ref query, includes);

            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy((o) => o.Id);
            }
            return await PagedList<T>.CreateAsync(query, pageNumber, pageSize, ct).ConfigureAwait(false);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default) =>
            predicate is null ? await _dbSet.CountAsync(ct).ConfigureAwait(false) : await _dbSet.CountAsync(predicate, ct).ConfigureAwait(false);

        public async Task<bool> Exist(Expression<Func<T, bool>> predicate, List<Expression<Func<T, object>>>? includes = null, CancellationToken ct = default)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
          
            var entity = await query.AsNoTracking().FirstOrDefaultAsync(predicate, ct).ConfigureAwait(false);

            return entity != null;
        }
        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity, ct).ConfigureAwait(false);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity); // or set IsDeleted = true for soft-delete
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            _dbSet.RemoveRange(entities);
        }

        public void SoftDelete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }


        private void AddIncudes(ref IQueryable<T> query, List<Expression<Func<T, object>>>? includes = null)
        {
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
        }
    }
}
