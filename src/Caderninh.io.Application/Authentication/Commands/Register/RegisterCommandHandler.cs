using ErrorOr;
using MediatR;
using Caderninh.io.Application.Authentication.Common;
using Caderninh.io.Application.Common.Interfaces;
using Caderninh.io.Domain.Common.Interfaces;
using Caderninh.io.Domain.Users;

namespace Caderninh.io.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        IJwtTokenGenerator _jwtTokenGenerator,
        IPasswordHasher _passwordHasher,
        IUsersRepository _usersRepository,
        IUnitOfWork _unitOfWork) : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (await _usersRepository.ExistsByEmailAsync(command.Email))
                return Error.Conflict(description: "User already exists");

            var hashPasswordResult = _passwordHasher.HashPassword(command.Password);

            if (hashPasswordResult.IsError)
                return hashPasswordResult.Errors;

            var user = new User(
                command.Name,
                command.Email,
                hashPasswordResult.Value);

            await _usersRepository.AddUserAsync(user);
            await _unitOfWork.CommitChangesAsync();

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}