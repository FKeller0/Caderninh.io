using Caderninh.io.Domain.User;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Caderninh.io.Infrastructure.Common.Persistence
{
    public class CaderninhoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public CaderninhoDbContext(DbContextOptions options) : base(options) { }

        public async Task CommitChangesAsync() 
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}