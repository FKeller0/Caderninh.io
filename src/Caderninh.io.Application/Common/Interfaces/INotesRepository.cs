using Caderninh.io.Domain.Notes;

namespace Caderninh.io.Application.Common.Interfaces
{
    public interface INotesRepository
    {
        Task AddNoteAsync(Note note);
        Task<bool> ExistsAsync(Guid id);
        Task<Note?> GetByIdAsync(Guid noteId);
        Task<List<Note>> ListByNoteCategoryIdAsync(Guid noteCategoryId);
        Task UpdateAsync(Note note);
        Task RemoveAsync(Note note);
        Task RemoveRangeAsync(List<Note> notes);
    }
}