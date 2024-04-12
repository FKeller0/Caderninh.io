using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using Caderninh.io.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Caderninh.io.Infrastructure.Notes.Persistence
{
    public class NotesRepository(CaderninhoDbContext dbContext) : INotesRepository
    {
        private readonly CaderninhoDbContext _dbContext = dbContext;

        public async Task AddNoteAsync(Note note)
        {
            await _dbContext.Notes.AddAsync(note);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Notes
                .AsNoTracking()
                .AnyAsync(note => note.Id == id);
        }

        public async Task<Note?> GetByIdAsync(Guid noteId)
        {
            return await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == noteId);
        }

        public async Task<List<Note>> ListByNoteCategoryIdAsync(Guid noteCategoryId)
        {
            return await _dbContext.Notes
                .AsNoTracking()
                .Where(note => note.NoteCategoryId == noteCategoryId)
                .ToListAsync();
        }
        public Task UpdateAsync(Note note)
        {
            _dbContext.Update(note);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Note note)
        {
            _dbContext.Notes.Remove(note);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Note> notes)
        {
            _dbContext.RemoveRange(notes);
            return Task.CompletedTask;
        }
    }
}