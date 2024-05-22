namespace Fire_Emblem;

public class BracingStance : DamageAlterationSkill
{
    private int bonus;
    public BracingStance(string name, string description) : base(name, description)
    {
        bonus = 6;
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Def", bonus);
            owner.AddTemporaryBonus("Res", bonus);

            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}