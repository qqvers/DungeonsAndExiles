namespace DungeonsAndExiles.Api.ViewModels
{
    public class ItemVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Damage { get; set; }
        public int Defence { get; set; }
    }
}
