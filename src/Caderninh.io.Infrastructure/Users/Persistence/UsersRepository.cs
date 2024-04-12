using Caderninh.io.Domain.Users;
using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Caderninh.io.Infrastructure.Users.Persistence
{
    public class UsersRepository(CaderninhoDbContext _dbContext) : IUsersRepository
    {
        public async Task AddUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbContext.Users.AnyAsync(user => user.Id == id);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public Task UpdateAsync(User user)
        {
            _dbContext.Update(user);

            return Task.CompletedTask;
        }
    }
}