namespace Fire_Emblem {
    public class FortDefRes : Skill {
        public FortDefRes(string name, string description) : base(name, description) {
        }

        public override void ApplyEffect(Battle battle, Character owner) {
            owner.AddTemporaryBonus("Def", 6);
            owner.AddTemporaryBonus("Res", 6);
            owner.AddTemporaryPenalty("Atk", -2);
        }
    }
}