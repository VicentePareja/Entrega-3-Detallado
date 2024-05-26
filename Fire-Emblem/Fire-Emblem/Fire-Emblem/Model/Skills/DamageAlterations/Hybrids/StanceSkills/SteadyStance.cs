namespace Fire_Emblem;

public class SteadyStance : DamageAlterationSkill
{
    private int bonus;
    private int reduction;
    public SteadyStance(string name, string description) : base(name, description)
    {
        bonus = 8;
        reduction = 10;
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;

        if (combat._attacker != owner)
        {

            owner.AddTemporaryBonus("Def", bonus);
            
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", reduction);
        }
    }
}