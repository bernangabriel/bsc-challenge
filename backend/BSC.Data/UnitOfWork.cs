using BSC.Core.Common.Contracts;
using Microsoft.EntityFrameworkCore.Storage;

namespace BSC.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;

        private readonly BscDataContext _dataContext;

        public UnitOfWork(BscDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IDbContextTransaction CreateTransaction()
        {
            return _dataContext.Database.BeginTransaction();
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _dataContext.Dispose();
            }

            _disposed = true;
        }
    }
}