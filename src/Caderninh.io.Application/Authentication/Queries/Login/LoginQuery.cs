using Caderninh.io.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Caderninh.io.Application.Authentication.Queries.Login
{
    public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}