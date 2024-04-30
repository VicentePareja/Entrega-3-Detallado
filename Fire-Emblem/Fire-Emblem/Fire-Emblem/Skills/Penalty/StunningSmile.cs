namespace Fire_Emblem {
    public class StunningSmile : Skill {
        public int Penalty { get; private set; }

        public StunningSmile(string name, string description) : base(name, description) {
            Penalty = -8;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;
            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            if (opponent.Gender == "Male") {
                opponent.AddTemporaryPenalty("Spd", Penalty);
            }
        }
    }
}