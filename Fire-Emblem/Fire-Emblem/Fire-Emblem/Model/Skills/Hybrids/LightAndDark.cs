namespace Fire_Emblem {
    public class LightAndDark : Skill {
        
        int _penalty = -5;
        public LightAndDark(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.CurrentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            AddPenalties(opponent);
            owner.DisableAllPenalties();
            opponent.DisableAllBonuses();
        }
        
        public void AddPenalties(Character opponent) {
            opponent.AddTemporaryPenalty("Atk", _penalty);
            opponent.AddTemporaryPenalty("Spd", _penalty);
            opponent.AddTemporaryPenalty("Def", _penalty);
            opponent.AddTemporaryPenalty("Res", _penalty);
        }
    }
}