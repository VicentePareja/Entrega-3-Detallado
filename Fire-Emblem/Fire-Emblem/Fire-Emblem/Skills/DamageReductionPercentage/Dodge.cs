using System;

namespace Fire_Emblem
{
    public class Dodge : Skill
    {
        public Dodge(string name, string description) : base(name, description) {}

        public override void ApplyEffect(Battle battle, Character owner)
        {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            int speedDifference = owner.Spd - opponent.Spd;
            if (speedDifference > 0)
            {
                int damageReductionPercentage = Math.Min(speedDifference * 4, 40);
                owner.MultiplyPercentageReduction("Damage", damageReductionPercentage);
            }
        }
    }
}