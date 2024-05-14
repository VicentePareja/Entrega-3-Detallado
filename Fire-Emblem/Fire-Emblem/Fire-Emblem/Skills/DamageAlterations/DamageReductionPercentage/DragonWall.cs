namespace Fire_Emblem 
{
    public class DragonWall : DamageAlterationSkill 
    {
        public DragonWall(string name, string description) : base(name, description) {}

        public override void ApplyEffect(Battle battle, Character owner) 
        {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            int resistanceDifference = owner.Res - opponent.Res;
            if (resistanceDifference > 0) 
                
            {
                int damageReductionPercentage = Math.Min(resistanceDifference * 4, 40);
                owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
            }
        }
    }
}