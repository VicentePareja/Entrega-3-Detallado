namespace Fire_Emblem {
    public class NotQuite : Skill {
        public int Penalty { get; private set; }

        public NotQuite(string name, string description) : base(name, description) {
            Penalty = -4;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            if (battle.CurrentCombat._defender == owner) {
                battle.CurrentCombat._attacker.AddTemporaryPenalty("Atk", Penalty);
            }
        }
    }
}