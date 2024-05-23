namespace DungeonsAndExiles.Api.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Damage { get; set; }
        public int Defence { get; set; }

        public Guid BackpackId { get; set; }
        public Backpack Backpack { get; set; }
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public Guid EquipmentId { get; set; }
        public Equipment Equipment { get; set; }
    }
}
