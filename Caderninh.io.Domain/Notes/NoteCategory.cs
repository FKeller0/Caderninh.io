using ErrorOr;
using Throw;

namespace Caderninh.io.Domain.Notes
{
    public class NoteCategory
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public List<Guid> _noteIds { get; set; } = new();

        public string Name { get; set; } = null!;

        public NoteCategory(string name, Guid userId)
        {
            Name = name;
            UserId = userId;
        }

        public ErrorOr<Success> AddNotes(Notes notes) 
        {
            _noteIds.Throw().IfContains(notes.Id);

            _noteIds.Add(notes.Id);

            return Result.Success;
        }
    }
}