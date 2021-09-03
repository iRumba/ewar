using System.Collections.Generic;

using Rpg.Client.Core;

namespace Rpg.Client.Models.Combat
{
    internal interface IInterationContext
    {
        IEnumerable<CombatUnit> GetEnemies();
    }
}