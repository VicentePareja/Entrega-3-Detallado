namespace Fire_Emblem {
    public class BracingBlow : BlowSkill {
        public int DefBonus { get; private set; }
        public int ResBonus { get; private set; }

        public BracingBlow(string name, string description) : base(name, description) {
            DefBonus = 6;
            ResBonus = 6;
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Def", DefBonus);
            owner.AddTemporaryBonus("Res", ResBonus);
        }
    }
}