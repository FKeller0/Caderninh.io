using Caderninh.io.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caderninh.io.Infrastructure.Users.Persistence
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name);

            builder.Property(u => u.Email);

            builder.Property("_passwordHash")
                .HasColumnName("PasswordHash");
        }
    }
}