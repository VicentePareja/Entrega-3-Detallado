namespace Fire_Emblem;

public class RemoteSturdy : Skill
{
    public RemoteSturdy(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;

        if (combat._attacker == owner)
        {
            owner.AddTemporaryBonus("Atk", 7);
            owner.AddTemporaryBonus("Def", 10);
            owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 30);
        }
    }
}