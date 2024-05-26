namespace Fire_Emblem;

public class BowGuard: DamageAlterationSkill
{
    public BowGuard(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
        bool isOpponentBow = opponent.Weapon == "Bow";
        if (isOpponentBow)
        {
            double damageReduction = -5.0;
            owner.AddTemporaryDamageAlteration("AbsoluteReduction", damageReduction);
        }
    }
}
