namespace Fire_Emblem;

public class DartingStance : Skill
{
    public DartingStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Spd", 8);
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}