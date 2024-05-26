namespace DungeonsAndExiles.Api.Models.Domain
{
    public class Backpack
    {
        public Guid Id { get; set; }
        public int Capacity {  get; set; }
        public Guid PlayerId { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
        public Player Player { get; set; }
    }
}
