namespace Fire_Emblem {
    public class BlindingFlash : Skill {
        public int Penalty { get; private set; }

        public BlindingFlash(string name, string description) : base(name, description) {
            Penalty = -4;
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            if (battle.CurrentCombat._attacker == owner) {
                battle.CurrentCombat._defender.AddTemporaryPenalty("Spd", Penalty);
            }
        }
    }
}