using Caderninh.io.Domain.Users;

namespace Caderninh.io.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}