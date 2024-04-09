using Caderninh.io.Domain.Users;

namespace Caderninh.io.Application.Authentication.Common
{
    public record AuthenticationResult(
        User User,
        string Token);    
}