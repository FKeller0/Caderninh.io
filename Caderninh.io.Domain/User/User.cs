using Caderninh.io.Domain.Common;

namespace Caderninh.io.Domain.User
{
    public class User : Entity
    {
        public string Name { get; set; } = null!;        
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}