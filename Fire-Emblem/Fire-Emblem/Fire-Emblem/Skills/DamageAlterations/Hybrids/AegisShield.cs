namespace Fire_Emblem;

public class AegisShield : DamageAlterationSkill
{
    public AegisShield(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        owner.AddTemporaryBonus("Def", 6);
        owner.AddTemporaryBonus("Res", 3);
        owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 50);
    }
}