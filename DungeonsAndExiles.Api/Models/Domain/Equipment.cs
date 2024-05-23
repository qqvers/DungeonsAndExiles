namespace DungeonsAndExiles.Api.Models.Domain
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }

        public List<Item> Items { get; set; }
        public Player Player { get; set; }

    }
}
