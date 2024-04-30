namespace Fire_Emblem 
{
    public class DragonWall : Skill 
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
                owner.MultiplyPercentageReduction("Damage", damageReductionPercentage);
                //Console.WriteLine($"dddd{opponent.Name} {opponent.DamagePercentageReduction["Damage"]}");
            }
        }
    }
}