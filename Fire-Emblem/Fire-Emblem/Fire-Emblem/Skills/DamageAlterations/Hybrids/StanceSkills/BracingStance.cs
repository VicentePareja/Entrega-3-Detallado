namespace Fire_Emblem;

public class BracingStance : Skill
{
    public BracingStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Def", 6);
            owner.AddTemporaryBonus("Res", 6);

            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}