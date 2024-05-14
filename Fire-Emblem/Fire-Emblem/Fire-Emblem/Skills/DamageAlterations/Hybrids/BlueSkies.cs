namespace Fire_Emblem;

public class BlueSkies : DamageAlterationSkill
{
    public BlueSkies(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        double damageReduction = -5.0; 
        double extraDamage = 5.0;      
        
        owner.AddTemporaryDamageAlteration("AbsoluteReduction", damageReduction);
        owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
    }
}