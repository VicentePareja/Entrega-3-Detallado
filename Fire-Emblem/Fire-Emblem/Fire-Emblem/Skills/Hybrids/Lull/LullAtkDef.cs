namespace Fire_Emblem {
    public class LullAtkDef : Skill {
        public LullAtkDef(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            opponent.AddTemporaryPenalty("Atk", -3);
            opponent.AddTemporaryPenalty("Def", -3);
            
            opponent.AreAtkBonusesEnabled = false;
            opponent.AreDefBonusesEnabled = false;
        }
    }
}