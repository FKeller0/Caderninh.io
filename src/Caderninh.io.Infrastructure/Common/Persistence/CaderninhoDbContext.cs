using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Caderninh.io.Domain.Users;
using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using System;
using System.Reflection.Metadata;

namespace Caderninh.io.Infrastructure.Common.Persistence
{
    public class CaderninhoDbContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<NoteCategory> NoteCategories { get; set; } = null!;
        public DbSet<Note> Notes { get; set; } = null!;

        public CaderninhoDbContext(DbContextOptions options) : base(options) { }

        public async Task CommitChangesAsync() 
        {
            await SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<NoteCategory>()
                .HasMany(e => e.Notes)
                .WithOne(e => e.NoteCategory)
                .HasForeignKey(e => e.NoteCategoryId)
                .IsRequired();

            modelBuilder.Entity<NoteCategory>()
                .HasIndex(n => new { n.Name, n.Id })
                .IsUnique(true);

            modelBuilder.Entity<Note>()
                .HasIndex(n => new { n.Body, n.Id })
                .IsUnique(true);

            base.OnModelCreating(modelBuilder);

        }
    }
}