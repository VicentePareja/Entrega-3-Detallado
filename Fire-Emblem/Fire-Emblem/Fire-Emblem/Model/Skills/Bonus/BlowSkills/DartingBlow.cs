namespace Fire_Emblem {
    public class DartingBlow : BlowSkill {
        public int Bonus { get; private set; }

        public DartingBlow(string name, string description) : base(name, description) {
            Bonus = 8; 
        }

        protected override void ApplySpecificEffect(Character owner) {
            owner.AddTemporaryBonus("Spd", Bonus);
        }
    }
}