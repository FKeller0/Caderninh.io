using Caderninh.io.Domain.Users;

namespace Caderninh.io.Application.Common.Interfaces
{
    public interface IUsersRepository
    {
        Task AddUserAsync(User user);
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
        Task UpdateAsync(User user);
    }
}