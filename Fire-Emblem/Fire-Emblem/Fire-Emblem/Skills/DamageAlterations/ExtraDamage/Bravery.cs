namespace Fire_Emblem;

public class Bravery : Skill
{
    public Bravery(string name, string description) : base(name, description)
    {
    }
    public override void ApplyEffect(Battle battle, Character owner)
    {
        double extraDamage = 5.0; // Daño extra fijo
        owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
    }
}