namespace Fire_Emblem;
public class ArmsShield : DamageAlterationSkill
{
    public ArmsShield(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        string advantage = combat._advantage;
        bool isOwnerAdvantage;
        
        if (advantage == "atacante")
        {
            isOwnerAdvantage = combat._attacker == owner;
        }
        else if (advantage == "defensor")
        {
            isOwnerAdvantage = combat._defender == owner;
        }else
        {
            isOwnerAdvantage = false;
        }
        
        if (isOwnerAdvantage)
        {
            double damageReduction = -7.0;
            owner.AddTemporaryDamageAlteration("AbsoluteReduction", damageReduction);
        }
    }
    
}