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

            builder.HasKey(n => n.Body);

            builder.HasKey(n => n.NoteCategoryId);
            builder.HasOne(x => x.NoteCategory)
                .WithMany(x => x.Notes)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.NoteCategoryId);

            builder.HasKey(n => n.CreatedAt);

            builder.HasKey(n => n.LastUpdatedAt);            
        }
    }
}