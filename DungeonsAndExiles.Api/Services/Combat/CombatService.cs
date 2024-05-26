using DungeonsAndExiles.Api.Models.Domain;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Services.Combat
{
    public class CombatService : ICombatService
    {
        private readonly Random _random;
        public CombatService()
        {
            _random = new Random();
        }
        public bool SimulateCombat(int playerStrength, int monsterStrength)
        {
            Random random = new Random();

            double playerWinProbability = 0.9;

            if (playerStrength > monsterStrength)
            {
                playerWinProbability += 0.1 * (playerStrength - monsterStrength) / playerStrength;
            }
            else
            {
                playerWinProbability -= 0.1 * (monsterStrength - playerStrength) / monsterStrength;
            }

            double roll = random.NextDouble();
            if (roll < playerWinProbability)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Item SimulateDropAfterCombat(List<Item> items)
        {
            if (_random.NextDouble() < 0.5)
            {
                return null;
            }
            else
            {
                int index = _random.Next(items.Count);
                return items[index];
            }
        }
    }
}
