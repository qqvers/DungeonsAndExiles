namespace DungeonsAndExiles.Api.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Damage { get; set; }
        public int Defence { get; set; }

        public List<Backpack>? Backpacks { get; set; }
        public List<Equipment>? Equipments { get; set; }
    }
}
