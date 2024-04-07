using Caderninh.io.Domain.Common;

namespace Caderninh.io.Domain.Notes
{
    public class Note : Entity
    {
        public Guid NoteCategoryId { get; }

        public string Body { get; set; } = null!;

        public DateTime CreatedAt { get; }

        public DateTime? UpdatedAt { get; set; }

        public Note(Guid noteCategoryId, string body, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            Body = body;
            NoteCategoryId = noteCategoryId;
            Id = id ?? Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
    }    
}