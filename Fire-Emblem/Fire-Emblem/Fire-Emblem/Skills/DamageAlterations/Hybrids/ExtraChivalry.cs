namespace Fire_Emblem {
    public class ExtraChivalry : DamageAlterationSkill {
        public ExtraChivalry(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            if (opponent.CurrentHP >= opponent.MaxHP * 0.5) {
                opponent.AddTemporaryPenalty("Atk", -5);
                opponent.AddTemporaryPenalty("Spd", -5);
                opponent.AddTemporaryPenalty("Def", -5);
            }
            
            int hpPercentage = (int)((double)opponent.CurrentHP / opponent.MaxHP * 100);
            int damageReductionPercentage = hpPercentage / 2;
            owner.MultiplyTemporaryDamageAlterations("PercentageReduction", damageReductionPercentage);
        }
    }
}