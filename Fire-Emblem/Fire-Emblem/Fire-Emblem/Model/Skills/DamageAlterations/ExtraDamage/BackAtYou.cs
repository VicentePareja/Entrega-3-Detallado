namespace Fire_Emblem;

public class BackAtYou : DamageAlterationSkill
{
    public BackAtYou(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        bool isInitiatorOpponent = combat._attacker != owner;
        if (isInitiatorOpponent)
        {
            double lostHP = owner.MaxHP - owner.CurrentHP;
            double extraDamage = Math.Round(lostHP * 0.5, 9);
            owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
        }
    }
}