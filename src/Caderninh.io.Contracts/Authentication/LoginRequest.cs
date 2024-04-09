namespace Caderninh.io.Contracts.Authentication
{
    public record LoginRequest(
        string Email,
        string Password);
}