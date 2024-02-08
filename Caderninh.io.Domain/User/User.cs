using Caderninh.io.Domain.Notes;
using ErrorOr;
using Throw;

namespace Caderninh.io.Domain.User
{
    public class User
    {
        public Guid Id { get; private set; }

        public List<Guid> _noteCategoryIds = new();

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; private set; } = null!;

        public string Password { get; private set; } = null!;

        public User() { }

        public User(string firstName, string lastName, string email, string password, Guid? id = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Id = id ?? Guid.NewGuid();
        }

        public ErrorOr<Success> AddNoteCategory(NoteCategory noteCategory)
        {
            _noteCategoryIds.Throw().IfContains(noteCategory.Id);

            _noteCategoryIds.Add(noteCategory.Id);

            return Result.Success;
        }
    }
}