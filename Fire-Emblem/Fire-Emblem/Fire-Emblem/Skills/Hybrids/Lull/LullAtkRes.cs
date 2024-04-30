namespace Fire_Emblem {
    public class LullAtkRes : Skill {
        public LullAtkRes(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            opponent.AddTemporaryPenalty("Atk", -3);
            opponent.AddTemporaryPenalty("Res", -3);
            
            opponent.AreAtkBonusesEnabled = false;
            opponent.AreResBonusesEnabled = false;
        }
    }
}