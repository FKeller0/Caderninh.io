using Caderninh.io.Domain.Notes;
using Caderninh.io.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caderninh.io.Application.Common.Interfaces
{
    public interface INoteCategoryRepository
    {
        Task AddNoteCategoryAsync(NoteCategory noteCategory);
        Task<bool> ExistsAsync(Guid id);

        //TODO - Validate if this is necessary
        //Task<bool> ExistsByNameAsync(Guid userId, string name);

        Task<NoteCategory?> GetByIdAsync(Guid id);
        Task<List<NoteCategory>> ListByUserIdAsync(Guid userId);
        Task UpdateAsync(NoteCategory noteCategory);
        Task RemoveAsync(NoteCategory noteCategory);
        Task RemoveRangeAsync(List<NoteCategory> noteCategories);
    }
}