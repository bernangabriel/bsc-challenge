using Microsoft.EntityFrameworkCore.Storage;

namespace BSC.Core.Common.Contracts
{
    public interface IUnitOfWork
    {
        IDbContextTransaction CreateTransaction();
        int SaveChanges();
    }
}