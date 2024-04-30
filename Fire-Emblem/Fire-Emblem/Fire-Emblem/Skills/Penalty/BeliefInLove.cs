namespace Fire_Emblem {
    public class BeliefInLove : Skill {
        public int Penalty { get; private set; }

        public BeliefInLove(string name, string description) : base(name, description) {
            Penalty = -5;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            Combat combat = battle.currentCombat;

            Character opponent = (combat._attacker == owner) ? combat._defender : combat._attacker;

            if (combat._attacker != owner || opponent.CurrentHP == opponent.MaxHP) {
                opponent.AddTemporaryPenalty("Atk", Penalty);
                opponent.AddTemporaryPenalty("Def", Penalty);
            }
        }
    }
}