using Caderninh.io.Domain.Common;

namespace Caderninh.io.Domain.Notes
{
    public class Note : Entity
    {
        public Guid NoteCategoryId { get; }

        public string Body { get; set; } = null!;

        public DateTime CreatedAt { get; }

        public DateTime LastUpdatedAt { get; set; }

        public NoteCategory NoteCategory { get; set; } = null!;

        public Note(
            Guid noteCategoryId,
            string body,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            Body = body;
            NoteCategoryId = noteCategoryId;
            Id = id ?? Guid.NewGuid();
            CreatedAt = DateTime.Now;
            LastUpdatedAt = DateTime.Now;
        }

        public Note() { }
    }    
}