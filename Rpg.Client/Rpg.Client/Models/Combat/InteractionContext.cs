using System.Collections.Generic;
using System.Linq;

using Rpg.Client.Core;

namespace Rpg.Client.Models.Combat
{
    internal sealed class InteractionContext : IInterationContext
    {
        private readonly ActiveCombat _combat;
        private readonly bool _isPlayer;

        public InteractionContext(ActiveCombat combat, bool isPlayer)
        {
            _combat = combat;
            _isPlayer = isPlayer;
        }

        public IEnumerable<CombatUnit> GetEnemies()
        {
            return _combat.Units.Where(x => x.Unit.IsPlayerControlled == !_isPlayer);
        }
    }
}