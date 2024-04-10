﻿using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Notes;
using Caderninh.io.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Caderninh.io.Infrastructure.NoteCategories.Persistence
{
    public class NoteCategoryRepository(CaderninhoDbContext dbContext) : INoteCategoryRepository
    {
        private readonly CaderninhoDbContext _dbContext = dbContext;

        public async Task AddNoteCategoryAsync(NoteCategory noteCategory)
        {
            await _dbContext.NoteCategories.AddAsync(noteCategory);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.NoteCategories
                .AsNoTracking()
                .AnyAsync(noteCategory => noteCategory.Id == id);
        }

        //public Task<bool> ExistsByNameAsync(Guid userId, string name)
        //{
        //    //TODO - Validate if this is necessary
        //    throw new NotImplementedException();
        //}

        public async Task<NoteCategory?> GetByIdAsync(Guid id)
        {
            return await _dbContext.NoteCategories
                .FirstOrDefaultAsync(noteCategory => noteCategory.Id == id);
        }

        public async Task<List<NoteCategory>> ListByUserIdAsync(Guid userId)
        {
            return await _dbContext.NoteCategories
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }
        public Task UpdateAsync(NoteCategory noteCategory)
        {
            _dbContext.Update(noteCategory);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(NoteCategory noteCategory)
        {
            _dbContext.Remove(noteCategory);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<NoteCategory> noteCategories)
        {
            _dbContext.RemoveRange(noteCategories);
            return Task.CompletedTask;
        }
    }
}