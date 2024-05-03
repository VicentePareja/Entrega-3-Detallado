namespace Fire_Emblem;

public class RemoteSparrow : Skill
{
    public RemoteSparrow(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        if (combat._attacker == owner)
        {
            owner.AddTemporaryBonus("Atk", 7);
            owner.AddTemporaryBonus("Spd", 7);
            owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 30);
        }
    }
}