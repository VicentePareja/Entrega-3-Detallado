namespace Fire_Emblem;

public class MoonTwinWing : DamageAlterationSkill
{
    public MoonTwinWing(string name, string description) : base(name, description) {}

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        ApplySpeedPenaltiesIfNecessary(owner, combat);
        ApplyDamageReductionBasedOnSpeedDifference(owner, combat);
    }
    
    private void ApplySpeedPenaltiesIfNecessary(Character owner, Combat combat)
    {
        Character opponent = GetOpponent(combat, owner);
        if (IsHealthAboveQuarter(owner))
        {
            ApplyStatPenalties(opponent, "Atk", -5);
            ApplyStatPenalties(opponent, "Spd", -5);
        }
    }
    
    private void ApplyDamageReductionBasedOnSpeedDifference(Character owner, Combat combat)
    {
        Character opponent = GetOpponent(combat, owner);
        int speedDifference = owner.GetEffectiveAttribute("Spd") - opponent.GetEffectiveAttribute("Spd");
        if (speedDifference > 0)
        {
            int damageReductionPercentage = CalculateDamageReductionPercentage(speedDifference);
            owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
        }
    }

    private bool IsHealthAboveQuarter(Character owner)
    {
        return Math.Round((double)owner.CurrentHP / owner.MaxHP, 9) >= 0.25;
    }

    private Character GetOpponent(Combat combat, Character owner)
    {
        return (combat._attacker == owner) ? combat._defender : combat._attacker;
    }

    private void ApplyStatPenalties(Character character, string attribute, int value)
    {
        character.AddTemporaryPenalty(attribute, value);
    }
    
    private int CalculateDamageReductionPercentage(int speedDifference)
    {
        int damageReductionPercentage = speedDifference * 4;
        return Math.Min(damageReductionPercentage, 40);
    }
}
