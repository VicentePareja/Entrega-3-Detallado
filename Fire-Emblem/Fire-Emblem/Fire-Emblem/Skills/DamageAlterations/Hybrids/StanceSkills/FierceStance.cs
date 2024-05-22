namespace Fire_Emblem;

public class FierceStance : DamageAlterationSkill
{
    int bonus;
    int reduction;
    public FierceStance(string name, string description) : base(name, description)
    {
        bonus = 8;
        reduction = 10;
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Atk", bonus);
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", reduction);
        }
    }
}