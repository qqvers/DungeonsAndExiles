using System.ComponentModel.DataAnnotations;

namespace DungeonsAndExiles.Api.Models.Domain
{
    public class User : Character
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public Guid RoleId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }

        public Role Role { get; set; } = null!;
        public List<Player>? Players { get; set; }
    }
}
