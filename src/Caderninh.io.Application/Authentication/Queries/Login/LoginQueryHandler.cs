using MediatR;
using Caderninh.io.Application.Authentication.Common;
using ErrorOr;
using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Common.Interfaces;

namespace Caderninh.io.Application.Authentication.Queries.Login
{
    public class LoginQueryHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IPasswordHasher _passwordHasher,
        IUsersRepository _usersRepository) : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetByEmailAsync(request.Email);

            return user is null || !user.IsCorrectPasswordHash(request.Password, _passwordHasher)
                ? AuthenticationErrors.InvalidCredentials
                : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
        }
    }
}