namespace Fire_Emblem {
    public class LullDefRes : Skill {
        public LullDefRes(string name, string description) : base(name, description) {
        }
        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            opponent.AddTemporaryPenalty("Def", -3);
            opponent.AddTemporaryPenalty("Res", -3);
            
            opponent.AreDefBonusesEnabled = false;
            opponent.AreResBonusesEnabled = false;
        }
    }
}