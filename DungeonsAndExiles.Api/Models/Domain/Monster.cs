namespace DungeonsAndExiles.Api.Models.Domain
{
    public class Monster : Character
    {
        public int Level { get; set; }
        public int Health { get; set; }
        public int Defence { get; set; }
        public int Damage { get; set; }
        public byte[] Image { get; set; } = null!;
    }
}
