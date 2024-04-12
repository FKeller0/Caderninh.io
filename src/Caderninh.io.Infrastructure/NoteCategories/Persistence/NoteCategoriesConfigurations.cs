using Caderninh.io.Domain.Notes;
using Caderninh.io.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caderninh.io.Infrastructure.NoteCategories.Persistence
{
    public class NoteCategoriesConfigurations : IEntityTypeConfiguration<NoteCategory>
    {
        public void Configure(EntityTypeBuilder<NoteCategory> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Id)
                .ValueGeneratedNever();

            builder.Property(n => n.UserId);            

            builder.Property(n => n.Name);            
        }
    }
}