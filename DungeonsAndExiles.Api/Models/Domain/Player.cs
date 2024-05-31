namespace DungeonsAndExiles.Api.Models.Domain
{
    public class Player : Character
    {
        public int Level { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Defence { get; set; } 
        public int Experience { get; set; }
        public int Stamina { get; set; }

        public Guid UserId { get; set; }
        public Guid BackpackId { get; set; }
        public Guid EquipmentId { get; set; }


        public User User { get; set; } = null!;
        public Backpack Backpack { get; set; } = null!;
        public Equipment Equipment { get; set; } = null!;
    }
}
