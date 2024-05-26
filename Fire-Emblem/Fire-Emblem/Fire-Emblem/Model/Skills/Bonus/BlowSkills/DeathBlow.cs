namespace Fire_Emblem {
    public class DeathBlow : BlowSkill {
        public int Bonus { get; private set; }

        public DeathBlow(string name, string description) : base(name, description) {
            Bonus = 8;
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Atk", Bonus);
        }
    }
}