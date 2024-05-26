namespace Fire_Emblem 
{
    public class GoldenLotus : DamageAlterationSkill 
    {
        public GoldenLotus(string name, string description) : base(name, description) {}

        public override void ApplyEffect(Battle battle, Character owner) 
        {
            Combat combat = battle.CurrentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            bool isOpponentPhysical = opponent.Weapon != "Magic";
            if (isOpponentPhysical) 
            {
                owner.MultiplyFirstAttackDamageAlterations("PercentageReduction", 50);
            }
        }
    }
}