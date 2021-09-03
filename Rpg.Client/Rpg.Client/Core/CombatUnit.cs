using System;
using System.Collections.Generic;

namespace Rpg.Client.Core
{
    internal class CombatUnit
    {
        public CombatUnit(Unit unit)
        {
            Unit = unit ?? throw new ArgumentNullException(nameof(unit));

            var cards = new List<CombatPowerCard>();
            foreach (var skill in Unit.Skills)
            {
                var card = new CombatPowerCard(skill);

                cards.Add(card);
            }

            CombatCards = cards;
        }

        public IEnumerable<CombatPowerCard>? CombatCards { get; }

        public Unit Unit { get; }
    }
}