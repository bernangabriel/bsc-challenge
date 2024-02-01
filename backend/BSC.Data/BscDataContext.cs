using BSC.Business.Entities;
using Microsoft.EntityFrameworkCore;

namespace BSC.Data
{
    public class BscDataContext : DbContext
    {
        #region Constructors

        public BscDataContext(DbContextOptions<BscDataContext> options)
            : base(options)
        {
        }

        #endregion

        #region Tables

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<EventLog> EventLogs { get; set; }

        #endregion

        #region Fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Save Changes

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(true, cancellationToken);
        }

        #endregion
    }
}