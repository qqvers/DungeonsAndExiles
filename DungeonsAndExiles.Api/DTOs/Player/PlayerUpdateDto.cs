using System.ComponentModel.DataAnnotations;

namespace DungeonsAndExiles.Api.DTOs.Player
{
    public class PlayerUpdateDto
    {
        [Required]
        public int Level { get; set; }
        [Required]
        public int Health { get; set; }
        [Required]
        public int Damage { get; set; }
        [Required]
        public int Defence { get; set; }
        [Required]
        public int Experience { get; set; }
    }
}
