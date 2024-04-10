using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Caderninh.io.Domain.Users;
using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;

namespace Caderninh.io.Infrastructure.Common.Persistence
{
    public class CaderninhoDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<NoteCategory> NoteCategories { get; set; } = null!;

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