namespace Fire_Emblem {
    public class WardingBlow : BlowSkill {
        public int Bonus { get; private set; }

        public WardingBlow(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Res", Bonus);
        }
    }
}