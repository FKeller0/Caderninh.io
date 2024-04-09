using Caderninh.io.Domain.Common;
using Caderninh.io.Domain.Common.Interfaces;

namespace Caderninh.io.Domain.Users
{
    public class User : Entity
    {
        public string Name { get; set; } = null!;        
        public string Email { get; set; } = null!;

        private readonly string _passwordHash = null!;

        public User(
            string name,
            string email,
            string passwordHash,
            Guid? id = null)
            : base(id ?? Guid.NewGuid())
        {
            Name = name;
            Email = email;
            _passwordHash = passwordHash;
        }

        public User() { }

        public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
        {
            return passwordHasher.IsCorrectPassword(password, _passwordHash);
        }
    }
}