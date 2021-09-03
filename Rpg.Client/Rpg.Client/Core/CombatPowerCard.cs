using System;

namespace Rpg.Client.Core
{
    internal class CombatPowerCard
    {
        public CombatPowerCard(CombatPower skill)
        {
            Scheme = skill ?? throw new ArgumentNullException(nameof(skill));
        }

        public CombatPower Scheme { get; }
    }
}