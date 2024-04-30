namespace Fire_Emblem {
    public class DisarmingSigh : Skill {
        public int Penalty { get; private set; }

        public DisarmingSigh(string name, string description) : base(name, description) {
            Penalty = -8;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            if (opponent.Gender == "Male") {
                opponent.AddTemporaryPenalty("Atk", Penalty);
            }
        }
    }
}