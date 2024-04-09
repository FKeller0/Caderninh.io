namespace Caderninh.io.Contracts.Authentication
{
    public record AuthenticationResponse(
    Guid Id,
    string Name,
    string Email,
    string Token);
}