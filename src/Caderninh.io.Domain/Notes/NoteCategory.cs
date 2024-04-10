using Caderninh.io.Domain.Common;
using ErrorOr;
using Throw;

namespace Caderninh.io.Domain.Notes
{
    public class NoteCategory : Entity
    {
        public Guid UserId { get; private set; }
        public List<Guid> _noteIds { get; set; } = [];

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

        public ErrorOr<Success> AddNotes(Note notes) 
        {
            _noteIds.Throw().IfContains(notes.Id);

            _noteIds.Add(notes.Id);

            return Result.Success;
        }
    }
}