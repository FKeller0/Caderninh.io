using Caderninh.io.Domain.Common;
using ErrorOr;
using Throw;

namespace Caderninh.io.Domain.Notes
{
    public class NoteCategory(string name, Guid userId, Guid? id = null) : Entity(id ?? Guid.NewGuid())
    {
        public Guid UserId { get; private set; } = userId;
        public List<Guid> _noteIds { get; set; } = [];

        public string Name { get; set; } = name;

        public ErrorOr<Success> AddNotes(Note notes) 
        {
            _noteIds.Throw().IfContains(notes.Id);

            _noteIds.Add(notes.Id);

            return Result.Success;
        }
    }
}