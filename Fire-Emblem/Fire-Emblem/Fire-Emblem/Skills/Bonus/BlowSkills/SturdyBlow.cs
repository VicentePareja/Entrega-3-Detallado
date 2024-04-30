namespace Fire_Emblem {
    public class SturdyBlow : BlowSkill {
        public int AtkBonus { get; private set; }
        public int DefBonus { get; private set; }

        public SturdyBlow(string name, string description) : base(name, description) {
            AtkBonus = 6;
            DefBonus = 6;
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Atk", AtkBonus);
            owner.AddTemporaryBonus("Def", DefBonus);
        }
    }
}