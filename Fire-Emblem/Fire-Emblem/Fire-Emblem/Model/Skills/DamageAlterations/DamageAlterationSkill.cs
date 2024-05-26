namespace Fire_Emblem;

public abstract class DamageAlterationSkill : Skill
{
    protected DamageAlterationSkill(string name, string description) : base(name, description)
    {
        IsDamageAlteration = true;
    }
    
}