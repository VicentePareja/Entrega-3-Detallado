namespace Fire_Emblem;

public class DartingStance : DamageAlterationSkill
{
    private int bonus;
    private int reduction;
    public DartingStance(string name, string description) : base(name, description)
    {
        bonus = 8;
        reduction = 10;
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Spd", bonus);
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", reduction);
        }
    }
}