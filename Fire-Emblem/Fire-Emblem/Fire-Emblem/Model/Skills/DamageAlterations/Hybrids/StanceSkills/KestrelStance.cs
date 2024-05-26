namespace Fire_Emblem;

public class KestrelStance : DamageAlterationSkill
{
    int bonus = 6;
    int reduction = 10;
    public KestrelStance(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;

        if (combat._attacker != owner)
        {

            owner.AddTemporaryBonus("Atk", bonus);
            owner.AddTemporaryBonus("Spd", bonus);

            owner.MultiplyFollowUpDamageAlterations("PercentageReduction", reduction);
        }
    }
}