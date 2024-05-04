namespace Fire_Emblem;

public class PoeticJustice : Skill
{
    public PoeticJustice(string name, string description) : base(name, description)
    {
    }

    public override void ApplyEffect(Battle battle, Character owner)
    {
        Combat combat = battle.currentCombat;
        Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

        opponent.AddTemporaryPenalty("Spd", -4);
        double extraDamage = (double)opponent.Atk * 0.15;
        owner.AddTemporaryDamageAlteration("ExtraDamage", extraDamage);
    }
}