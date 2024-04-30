namespace Fire_Emblem {
    public class StillWater : Skill {
        public StillWater(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Atk", 6);
            owner.AddTemporaryBonus("Res", 6);
            owner.AddTemporaryPenalty("Def", -5);
        }
    }
}