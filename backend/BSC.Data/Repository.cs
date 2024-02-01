using BSC.Core.Common.Base;

namespace BSC.Data
{
    public class Repository<TEntity> : RepositoryBase<TEntity, BscDataContext>
        where TEntity : class, new()
    {
        public Repository(BscDataContext context) : base(context)
        {
        }
    }
}