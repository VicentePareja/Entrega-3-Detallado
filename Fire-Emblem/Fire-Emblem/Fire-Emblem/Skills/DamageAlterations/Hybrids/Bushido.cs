namespace Fire_Emblem;

public class Bushido : DamageAlterationSkill
{
    public Bushido(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        double extraDamage = 7.0;
        owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);

        Combat combat = battle.currentCombat;
        Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
        int speedDifference = owner.Spd - opponent.Spd;

        if (speedDifference > 0)
        {
            int damageReductionPercentage = speedDifference * 4;
            if (damageReductionPercentage > 40) damageReductionPercentage = 40;

            owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
        }
    }
}