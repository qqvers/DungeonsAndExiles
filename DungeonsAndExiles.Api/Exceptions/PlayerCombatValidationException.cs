namespace DungeonsAndExiles.Api.Exceptions
{
    public class PlayerCombatValidationException : Exception
    {
        public PlayerCombatValidationException() { }
        public PlayerCombatValidationException(string message) : base(message)
        {
        }
    }

}
