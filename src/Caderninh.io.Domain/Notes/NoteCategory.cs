using Caderninh.io.Domain.Common;
using ErrorOr;
using Throw;

namespace Caderninh.io.Domain.Notes
{
    public class NoteCategory : Entity
    {
        public Guid UserId { get; private set; }

        public ICollection<Note> Notes { get; } = new List<Note>();

        public string Name { get; set; } = null!;

        public NoteCategory(
            string name,
            Guid userId,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            Name = name;
            UserId = userId;
        }

        public NoteCategory() { }
    }
}