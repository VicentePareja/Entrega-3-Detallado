namespace Fire_Emblem;

public class RemoteSparrow : DamageAlterationSkill
{
    public RemoteSparrow(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        if (combat._attacker == owner)
        {
            owner.AddTemporaryBonus("Atk", 7);
            owner.AddTemporaryBonus("Spd", 7);
            owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 30);
        }
    }
}