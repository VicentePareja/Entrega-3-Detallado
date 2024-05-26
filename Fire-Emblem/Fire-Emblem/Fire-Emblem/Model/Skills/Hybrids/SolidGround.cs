namespace Fire_Emblem {
    public class SolidGround : Skill {
        public SolidGround(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Atk", 6);
            owner.AddTemporaryBonus("Def", 6);
            
            owner.AddTemporaryPenalty("Res", -5);
        }
    }
}