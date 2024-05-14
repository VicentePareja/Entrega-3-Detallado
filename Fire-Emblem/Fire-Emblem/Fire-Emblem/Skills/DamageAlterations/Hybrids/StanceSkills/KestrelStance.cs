namespace Fire_Emblem;

public class KestrelStance : DamageAlterationSkill
{
    public KestrelStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;

        if (combat._attacker != owner)
        {

            owner.AddTemporaryBonus("Atk", 6);
            owner.AddTemporaryBonus("Spd", 6);

            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", 10);
        }
    }
}