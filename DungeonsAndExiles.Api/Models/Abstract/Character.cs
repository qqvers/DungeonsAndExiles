namespace DungeonsAndExiles.Api.Models.Domain
{
    public abstract class Character
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
