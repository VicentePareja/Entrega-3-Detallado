namespace Fire_Emblem;

public class SteadyPosture : Skill
{
    public SteadyPosture(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Spd", 6);
            owner.AddTemporaryBonus("Def", 6);
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}