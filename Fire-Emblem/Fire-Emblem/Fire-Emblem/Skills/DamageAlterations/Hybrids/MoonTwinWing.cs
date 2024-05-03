namespace Fire_Emblem;

public class MoonTwinWing : Skill
{
    public MoonTwinWing(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
        
        if (Math.Round((double)owner.CurrentHP / owner.MaxHP,2) >= 0.25)
        {
            opponent.AddTemporaryPenalty("Atk", -5);
            opponent.AddTemporaryPenalty("Spd", -5);
        }
        
        int speedDifference = owner.Spd - opponent.Spd;
        if (speedDifference > 0)
        {
            int damageReductionPercentage = speedDifference * 4;
            if (damageReductionPercentage > 40) damageReductionPercentage = 40;
            owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
        }
    }
}