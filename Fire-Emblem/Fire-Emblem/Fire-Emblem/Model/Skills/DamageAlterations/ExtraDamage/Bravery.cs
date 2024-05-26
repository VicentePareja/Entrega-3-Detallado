namespace Fire_Emblem;

public class Bravery : DamageAlterationSkill
{
    public Bravery(string name, string description) : base(name, description)
    {
    }
    public override void ApplyEffect(Battle battle, Character owner)
    {
        double extraDamage = 5.0;
        owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
    }
}