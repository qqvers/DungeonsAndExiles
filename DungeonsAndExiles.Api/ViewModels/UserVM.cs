namespace DungeonsAndExiles.Api.ViewModels
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
