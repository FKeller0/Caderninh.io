namespace Caderninh.io.Contracts.Authentication
{
    public record RegisterRequest(
        string Name,
        string Email,
        string Password);
}