namespace DungeonsAndExiles.Api.Models.Authentication
{
    public class TokenResult
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration {  get; set; }
    }
}
