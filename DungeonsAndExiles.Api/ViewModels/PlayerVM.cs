namespace DungeonsAndExiles.Api.ViewModels
{
    public class PlayerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Level { get; set; }
    }
}
