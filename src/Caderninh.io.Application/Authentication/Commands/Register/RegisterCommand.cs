using ErrorOr;
using MediatR;
using Caderninh.io.Application.Authentication.Common;

namespace Caderninh.io.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
    string Name,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}