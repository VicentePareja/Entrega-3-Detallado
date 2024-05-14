namespace Fire_Emblem;

public class WardingStance : DamageAlterationSkill
{
    public WardingStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;

        if (combat._attacker != owner)
        {

            owner.AddTemporaryBonus("Res", 8);
            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}