namespace Fire_Emblem {
    public class LullSpdDef : Skill {
        public LullSpdDef(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            opponent.AddTemporaryPenalty("Spd", -3);
            opponent.AddTemporaryPenalty("Def", -3);
            
            opponent.AreSpdBonusesEnabled = false;
            opponent.AreDefBonusesEnabled = false;
        }
    }
}