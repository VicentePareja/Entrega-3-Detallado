namespace Fire_Emblem;

public class AxeGuard : DamageAlterationSkill
{
    public AxeGuard(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.CurrentCombat;
        Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
        bool isOpponentAxe = opponent.Weapon == "Axe";
        if (isOpponentAxe)
        {
            double damageReduction = -5.0;
            owner.AddTemporaryDamageAlteration("AbsoluteReduction", damageReduction);
        }
    }
}