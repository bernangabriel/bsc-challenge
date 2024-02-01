using System.Linq.Expressions;

namespace BSC.Core.Common.Contracts
{
    public interface IDataRepository
    {
    }

    public interface IDataRepository<T> : IDataRepository
        where T : class, new()
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entityList);
        Task RemoveAllAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entityList);
        Task<IEnumerable<T>> GetAllAsync(string? sortExpression = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, string? sortExpression = null);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> transform,
            Expression<Func<T, bool>>? filter = null, string? sortExpression = null);

        IEnumerable<T> GetAll(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>> filter = null,
            string sortExpression = null);

        Task<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>>? filter = null, string? sortExpression = null);

        Task<IPagedList<T>> GetPagedAsync(int pageIndex, int pageSize, string? sortExpression = null);

        Task<IPagedList<T>> GetPagedAsync(Func<IQueryable<T>, IQueryable<T>> transform,
            Expression<Func<T, bool>>? filter = null, int pageIndex = -1, int pageSize = -1,
            string? sortExpression = null);

        Task<IPagedList<TResult>> GetPagedAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>>? filter = null, int pageIndex = -1, int pageSize = -1,
            string? sortExpression = null);

        Task<int> GetCountAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>>? filter = null);

        Task<TResult> GetSingleAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>>? filter = null, string? sortExpression = null);

        Task<TResult> GetFirstOrDefaultAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>> filter = null, string sortExpression = null);

        Task<bool> ExistsAsync<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>>? filter = null);

        Task<bool> ExistsAsync(Func<IQueryable<T>, IQueryable<T>> transform, Expression<Func<T, bool>>? filter = null);

        IEnumerable<TResult> GetAll<TResult>(Func<IQueryable<T>, IQueryable<TResult>> transform,
            Expression<Func<T, bool>> filter = null, string sortExpression = null);
    }
}