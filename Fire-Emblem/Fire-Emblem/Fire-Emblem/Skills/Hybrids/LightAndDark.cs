namespace Fire_Emblem {
    public class LightAndDark : Skill {
        public LightAndDark(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;
            
            opponent.AddTemporaryPenalty("Atk", -5);
            opponent.AddTemporaryPenalty("Spd", -5);
            opponent.AddTemporaryPenalty("Def", -5);
            opponent.AddTemporaryPenalty("Res", -5);
            
            owner.DisableAllPenalties();
            opponent.DisableAllBonuses();
        }
    }
}