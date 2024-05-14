namespace Fire_Emblem;

public class SwiftStance : DamageAlterationSkill
{
    public SwiftStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        if (combat._attacker != owner)
        {
            owner.AddTemporaryBonus("Spd", 6);
            owner.AddTemporaryBonus("Res", 6);
            
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}