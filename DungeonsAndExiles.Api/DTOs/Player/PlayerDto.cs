using System.ComponentModel.DataAnnotations;

namespace DungeonsAndExiles.Api.DTOs.Player
{
    public class PlayerDto
    {
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 20 characters.")]
        public string Name { get; set; } = null!;
    }
}
