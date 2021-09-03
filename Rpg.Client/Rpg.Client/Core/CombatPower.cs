namespace Rpg.Client.Core
{
    internal sealed class CombatPower
    {
        public int DamageMax { get; set; }
        public int DamageMaxPerLevel { get; set; }
        public int DamageMin { get; set; }
        public int DamageMinPerLevel { get; set; }

        public CombatPowerTarget Target { get; set; }

        public CombatPower()
        {
            Target = CombatPowerTarget.Single;
        }
    }
}