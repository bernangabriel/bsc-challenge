using BSC.Core.Common.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BSC.Data
{
    /// <summary>
    /// Importable class that represents a factory of Repositories
    /// </summary>
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private readonly IServiceProvider _services;

        public DataRepositoryFactory()
        {
        }

        public DataRepositoryFactory(IServiceProvider services)
        {
            _services = services;
        }

        public IDataRepository<TEntity> GetDataRepository<TEntity>() where TEntity : class, new()
        {
            //Import instance of T from the DI container
            var instance = _services.GetService<IDataRepository<TEntity>>();

            return instance;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            //Import instance of T from the DI container
            var instance = _services.GetService<IUnitOfWork>();

            return instance;
        }

        public IUnitOfWork GetUnitOfWork<T>() where T : IUnitOfWork
        {
            //Import instance of T from the DI container
            var instance = _services.GetService<T>();

            return instance;
        }
    }
}