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

        Combat combat = battle.CurrentCombat;
        Character opponent = GetOpponent(combat, owner);
        ApplySpeedBasedDamageReduction(owner, opponent);
    }

    private Character GetOpponent(Combat combat, Character owner)
    {
        return (combat._attacker == owner) ? combat._defender : combat._attacker;
    }

    private void ApplySpeedBasedDamageReduction(Character owner, Character opponent)
    {
        int speedDifference = owner.GetEffectiveAttribute("Spd") - opponent.GetEffectiveAttribute("Spd");
        if (speedDifference > 0)
        {
            int damageReductionPercentage = speedDifference * 4;
            damageReductionPercentage = Math.Min(damageReductionPercentage, 40);  // Ensure max reduction is capped at 40%
            owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
        }
    }
}