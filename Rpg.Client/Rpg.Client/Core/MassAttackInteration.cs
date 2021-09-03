using System;
using System.Collections.Generic;

namespace Rpg.Client.Core
{
    internal sealed class MassAttackInteration : IPowerInteraction
    {
        private readonly CombatPowerCard _combatSkillCard;
        private readonly Action _postExecute;
        private readonly IEnumerable<CombatUnit> _targets;

        public MassAttackInteration(IEnumerable<CombatUnit> targets, CombatPowerCard combatSkillCard, Action postExecute)
        {
            _targets = targets;
            _combatSkillCard = combatSkillCard;
            _postExecute = postExecute;
        }

        public void Execute()
        {
            foreach (var target in _targets)
            {
                target.Unit.TakeDamage(_combatSkillCard.Scheme.DamageMin);
                _postExecute?.Invoke();
            }
        }
    }
}