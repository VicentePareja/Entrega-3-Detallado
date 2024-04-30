namespace Fire_Emblem {
    public class Luna : Skill {
        public Luna(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            int penaltyDef = -(int)Math.Floor(opponent.Def * 0.5);
            int penaltyRes = -(int)Math.Floor(opponent.Res * 0.5);
            
            opponent.AddTemporaryFirstAttackPenalties("Def", penaltyDef);
            opponent.AddTemporaryFirstAttackPenalties("Res", penaltyRes);
        }
    }
}