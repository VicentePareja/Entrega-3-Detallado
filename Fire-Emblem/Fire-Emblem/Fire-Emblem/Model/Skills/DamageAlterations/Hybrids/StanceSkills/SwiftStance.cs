namespace Fire_Emblem;

public class SwiftStance : DamageAlterationSkill
{
    int bonus;
    int reduction;
    public SwiftStance(string name, string description) : base(name, description)
    {
        bonus = 6;
        reduction = 10;
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Spd", bonus);
            owner.AddTemporaryBonus("Res", bonus);
            
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", reduction);
        }
    }
}