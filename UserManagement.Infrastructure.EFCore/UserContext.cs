using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Aggregates.Users;
using UserManagement.Domain.Aggregates.Users.ValueObjects;
using UserManagement.Infrastructure.EFCore.Mappings;

namespace UserManagement.Infrastructure.EFCore
{
    public class UserContext : DbContext
    {
        private static readonly System.Type[] EnumerationTypes =
    { typeof(Role) };


        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var asswmbly = typeof(UserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(asswmbly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var enumerationEntries =
            ChangeTracker.Entries()
                .Where(current => EnumerationTypes.Contains(current.Entity.GetType()));

            foreach (var enumerationEntry in enumerationEntries)
            {
                enumerationEntry.State = EntityState.Unchanged;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
