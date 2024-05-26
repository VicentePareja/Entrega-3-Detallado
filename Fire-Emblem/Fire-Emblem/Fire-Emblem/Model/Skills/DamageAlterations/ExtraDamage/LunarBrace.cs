namespace Fire_Emblem;

public class LunarBrace : DamageAlterationSkill
{
    public LunarBrace(string name, string description) : base(name, description) {}

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        bool isOwnerInitiator = combat._attacker == owner;
        bool isPhysicalAttack = owner.Weapon != "Magic";

        if (IsEffectApplicable(isOwnerInitiator, isPhysicalAttack))
        {
            Character opponent = combat._defender;
            double extraDamage = opponent.GetEffectiveAttribute("Def") * 0.3;
            owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
        }
    }
    
    private bool IsEffectApplicable(bool isOwnerInitiator, bool isPhysicalAttack)
    {
        return isOwnerInitiator && isPhysicalAttack;
    }
    
    
}