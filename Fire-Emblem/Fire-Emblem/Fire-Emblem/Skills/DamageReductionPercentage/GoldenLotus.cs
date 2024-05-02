namespace Fire_Emblem 
{
    public class GoldenLotus : Skill 
    {
        public GoldenLotus(string name, string description) : base(name, description) {}

        public override void ApplyEffect(Battle battle, Character owner) 
        {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            bool isOpponentPhysical = opponent.Weapon != "Magic";
            if (isOpponentPhysical) 
            {
                owner.MultiplyfirstAttackDamageAlterations("PercentageReduction", 50);
            }
        }
    }
}