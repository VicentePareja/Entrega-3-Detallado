namespace Fire_Emblem;

public class BackAtYou : Skill
{
    public BackAtYou(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        bool isInitiatorOpponent = combat._attacker != owner;
        if (isInitiatorOpponent)
        {
            double lostHP = owner.MaxHP - owner.CurrentHP;
            double extraDamage = lostHP / 2;
            owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
        }
    }
}