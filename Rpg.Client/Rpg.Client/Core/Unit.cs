using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpg.Client.Core
{
    internal class Unit
    {
        public Unit(UnitScheme unitScheme, int combatLevel)
        {
            UnitScheme = unitScheme;
            CombatLevel = combatLevel;

            InitStats(unitScheme, combatLevel);
        }

        public int CombatLevel { get; set; }

        public int Hp { get; set; }

        public bool IsDead => Hp <= 0;

        public bool IsPlayerControlled { get; set; }
        public int MaxHp { get; set; }

        public IEnumerable<CombatPower> Skills { get; set; }

        public UnitScheme UnitScheme { get; init; }

        public int Xp { get; set; }

        public void GainXp(int amount)
        {
            Xp += amount;

            var xpToLevel = 100 + CombatLevel * 100;
            if (Xp >= xpToLevel)
            {
                CombatLevel++;
                Xp -= xpToLevel;

                InitStats(UnitScheme, CombatLevel);
            }
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            DamageTaken?.Invoke(this, EventArgs.Empty);
        }

        private void InitStats(UnitScheme unitScheme, int combatLevel)
        {
            MaxHp = unitScheme.Hp + unitScheme.HpPerLevel * CombatLevel;
            Hp = MaxHp;

            Skills = unitScheme.Skills.Select(x => new CombatPower
            {
                DamageMin = x.DamageMin + x.DamageMinPerLevel * combatLevel,
                DamageMax = x.DamageMax + x.DamageMaxPerLevel * combatLevel,
                Target = x.Target
            }).ToArray();
        }

        public event EventHandler DamageTaken;
    }
}