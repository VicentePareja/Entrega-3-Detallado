namespace Fire_Emblem;
public class ArmsShield : DamageAlterationSkill
{
    public ArmsShield(string name, string description) : base(name, description)
    {
        
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        if (HasOwnerAdvantage(combat, owner))
        {
            double damageReduction = -7.0;
            owner.AddTemporaryDamageAlteration("AbsoluteReduction", damageReduction);
        }
    }
    
    private bool HasOwnerAdvantage(Combat combat, Character owner)
    {
        string advantage = combat._advantage;
        return (advantage == "atacante" && combat._attacker == owner) ||
               (advantage == "defensor" && combat._defender == owner);
    }
}