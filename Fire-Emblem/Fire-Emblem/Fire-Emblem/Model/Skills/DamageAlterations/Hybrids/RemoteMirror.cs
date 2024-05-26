namespace Fire_Emblem;

public class RemoteMirror : DamageAlterationSkill
{
    public RemoteMirror(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;

        if (combat._attacker == owner)
        {

            owner.AddTemporaryBonus("Atk", 7);
            owner.AddTemporaryBonus("Res", 10);
            owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 30);
        }
    }
}