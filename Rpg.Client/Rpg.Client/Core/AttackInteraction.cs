using System;

namespace Rpg.Client.Core
{
    internal sealed class AttackInteraction : IPowerInteraction
    {
        private readonly CombatPowerCard _combatSkillCard;
        private readonly Action _postExecute;
        private readonly CombatUnit _target;

        public AttackInteraction(CombatUnit target, CombatPowerCard combatSkillCard, Action postExecute)
        {
            _target = target;
            _combatSkillCard = combatSkillCard;
            _postExecute = postExecute;
        }

        public void Execute()
        {
            _target.Unit.TakeDamage(_combatSkillCard.Scheme.DamageMin);
            _postExecute?.Invoke();
        }
    }
}