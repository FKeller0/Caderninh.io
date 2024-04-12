using Caderninh.io.Domain.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Caderninh.io.Infrastructure.Notes.Persistence
{
    public class NotesConfigurations : IEntityTypeConfiguration<Note>
    {        
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(n => n.Id);

            builder.Property(n => n.Id)
                .ValueGeneratedNever();

            builder.Property(n => n.Body);

            builder.Property(n => n.NoteCategoryId);
            builder.HasOne(x => x.NoteCategory)
                .WithMany(x => x.Notes)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.NoteCategoryId);

            builder.Property(n => n.CreatedAt);

            builder.Property(n => n.UpdatedAt);            
        }
    }
}