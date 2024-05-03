namespace Fire_Emblem;

public class SteadyStance : Skill
{
    public SteadyStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;

        if (combat._attacker != owner)
        {

            owner.AddTemporaryBonus("Def", 8);
            
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}