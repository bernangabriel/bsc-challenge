namespace BSC.Core.Common.Contracts
{
    public interface IDataRepositoryFactory
    {
        IDataRepository<TEntity> GetDataRepository<TEntity>() where TEntity : class, new();
        IUnitOfWork GetUnitOfWork();
        IUnitOfWork GetUnitOfWork<T>() where T : IUnitOfWork;
    }
}