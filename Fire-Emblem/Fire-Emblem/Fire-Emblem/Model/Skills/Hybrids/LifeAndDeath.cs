namespace Fire_Emblem {
    public class LifeAndDeath : Skill {
        public LifeAndDeath(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Atk", 6);
            owner.AddTemporaryBonus("Spd", 6);
            owner.AddTemporaryPenalty("Def", -5);
            owner.AddTemporaryPenalty("Res", -5);
        }
    }
}