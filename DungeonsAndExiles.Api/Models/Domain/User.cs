namespace DungeonsAndExiles.Api.Models.Domain
{
    public class User : Character
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Player> Players { get; set; }
    }
}
