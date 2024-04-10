using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Caderninh.io.Contracts.Authentication;
using Caderninh.io.Application.Authentication.Commands.Register;
using Caderninh.io.Application.Authentication.Common;
using Caderninh.io.Application.Authentication.Queries.Login;

namespace Caderninh.io.API.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthenticationController (ISender _mediator) : ApiController
    {        

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request) 
        {
            var command = new RegisterCommand(
                request.Name,
                request.Email,
                request.Password);

            var authResult = await _mediator.Send(command);

            return authResult.Match(
                authResult => base.Ok(MapToAuthResponse(authResult)),
                Problem);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request) 
        {
            var query = new LoginQuery(request.Email, request.Password);

            var authResult = await _mediator.Send(query);

            if (authResult.IsError && authResult.FirstError == AuthenticationErrors.InvalidCredentials) 
            {
                return Problem(
                    detail: authResult.FirstError.Description,
                    statusCode: StatusCodes.Status401Unauthorized);
            }                

            return authResult.Match(
                authResult => base.Ok(MapToAuthResponse(authResult)),
                Problem);
        }

        private static AuthenticationResponse MapToAuthResponse(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.Name,
                authResult.User.Email,
                authResult.Token);
        }
    }
}