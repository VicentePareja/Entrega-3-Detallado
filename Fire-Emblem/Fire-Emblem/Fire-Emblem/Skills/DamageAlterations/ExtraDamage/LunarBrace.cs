namespace Fire_Emblem;

public class LunarBrace : DamageAlterationSkill
{
    public LunarBrace(string name, string description) : base(name, description) {}

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        bool isOwnerInitiator = combat._attacker == owner;
        bool isPhysicalAttack = owner.Weapon != "Magic";

        if (isOwnerInitiator && isPhysicalAttack)
        {
            Character opponent = combat._defender;
            double extraDamage = opponent.Def * 0.3;
            owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
        }
    }
}