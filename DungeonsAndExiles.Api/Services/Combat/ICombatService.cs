using DungeonsAndExiles.Api.Models.Domain;
using System;
using System.Threading.Tasks;

namespace DungeonsAndExiles.Api.Services
{
    public interface ICombatService
    {
        public bool SimulateCombat(int playerStrength, int monsterStrength);
        public Item SimulateDropAfterCombat(List<Item> items);
    }
}
