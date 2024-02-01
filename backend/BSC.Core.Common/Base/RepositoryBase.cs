using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using BSC.Core.Common.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BSC.Core.Common.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<TEntity, TU> : IDataRepository<TEntity>
        where TEntity : class, new()
        where TU : DbContext
    {
        protected readonly TU Context;
        private readonly DbSet<TEntity> _DbSet;

        protected RepositoryBase(TU context)
        {
            Context = context;
            _DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);

            await Context.SaveChangesAsync();

            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entityList)
        {
            Context.Set<TEntity>().AddRange(entityList);

            Context.SaveChanges();

            return entityList;
        }

        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entityList)
        {
            await Context.Set<TEntity>().AddRangeAsync(entityList);

            await Context.SaveChangesAsync();

            return entityList;
        }

        public virtual async Task RemoveAsync(TEntity entity)
        {
            _DbSet.Attach(entity);

            Context.Entry<TEntity>(entity).State = EntityState.Deleted;

            await Context.SaveChangesAsync();
        }

        public virtual async Task RemoveAllAsync(IEnumerable<TEntity> entities)
        {
            foreach (TEntity current in entities)
            {
                _DbSet.Attach(current);
                Context.Entry<TEntity>(current).State = EntityState.Deleted;
            }

            await Context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _DbSet.Attach(entity);
            Context.Entry<TEntity>(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entityList)
        {
            foreach (var entity in entityList)
            {
                _DbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }

            await Context.SaveChangesAsync();

            return entityList;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string sortExpression = null)
        {
            var query = _DbSet.AsNoTracking();

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter,
            string sortExpression = null)
        {
            var query = _DbSet.AsNoTracking().Where(filter);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null,
            string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await notSortedResults.ToListAsync();
        }

        public virtual async Task<IEnumerable<TResult>> GetAllAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null,
            string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await notSortedResults.ToListAsync();
        }

        public virtual IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform,
            Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return notSortedResults.ToList();
        }

        public virtual IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
            Expression<Func<TEntity, bool>> filter = null, string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return notSortedResults.ToList();
        }

        public virtual IPagedList<TEntity> GetPaged(int pageIndex, int pageSize, string sortExpression = null)
        {
            var query = _DbSet.AsNoTracking();

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return new PagedList<TEntity>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<TEntity> GetPaged(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform,
            Expression<Func<TEntity, bool>> filter = null, int pageIndex = -1, int pageSize = -1,
            string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return new PagedList<TEntity>(notSortedResults, pageIndex, pageSize);
        }

        public virtual IPagedList<TResult> GetPaged<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
            Expression<Func<TEntity, bool>> filter = null, int pageIndex = -1, int pageSize = -1,
            string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return new PagedList<TResult>(notSortedResults, pageIndex, pageSize);
        }

        public virtual async Task<IPagedList<TEntity>> GetPagedAsync(int pageIndex, int pageSize,
            string sortExpression = null)
        {
            return await Task.Run(() => this.GetPaged(pageIndex, pageSize, sortExpression));
        }

        public virtual async Task<IPagedList<TEntity>> GetPagedAsync(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null,
            int pageIndex = -1, int pageSize = -1, string sortExpression = null)
        {
            return await Task.Run(() => this.GetPaged(transform, filter, pageIndex, pageSize, sortExpression));
        }

        public virtual async Task<IPagedList<TResult>> GetPagedAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null,
            int pageIndex = -1, int pageSize = -1, string sortExpression = null)
        {
            return await Task.Run(() => this.GetPaged(transform, filter, pageIndex, pageSize, sortExpression));
        }


        public virtual async Task<int> GetCountAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            return await transform(query).CountAsync();
        }

        public virtual async Task<TResult> GetSingleAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null,
            string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await notSortedResults.SingleOrDefaultAsync();
        }

        public virtual async Task<TResult> GetFirstOrDefaultAsync<TResult>(
            Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null,
            string sortExpression = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            //Breaking Changes in EF Core 3: The Query Translator for EF Core 3 was changed and the query building
            //and evaluation is different. Now, when you try to order by a property name string then it crashes.
            //If an update fixes this or a workaround is found, then implement it into the OrderBy Extension
            //and uncomment the line below.

            return await notSortedResults.FirstOrDefaultAsync();
        }

        public virtual async Task<bool> ExistsAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var result = transform(query);

            return await result.AnyAsync();
        }

        public virtual async Task<bool> ExistsAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform,
            Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var result = transform(query);

            return await result.AnyAsync();
        }
    }
}