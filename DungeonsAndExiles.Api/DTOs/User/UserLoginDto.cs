using System.ComponentModel.DataAnnotations;

namespace DungeonsAndExiles.Api.DTOs.User
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(30, ErrorMessage = "Email cannot exceed 30 characters.")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Password must be between 5 and 20 characters.")]
        public string Password { get; set; }
    }
}
